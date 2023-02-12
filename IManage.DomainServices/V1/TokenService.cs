using IManage.Domain.V1;
using IManage.ErrorHandling.ApiExceptions;
using IManage.Interfaces.V1.Repositories;
using IManage.Interfaces.V1.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using IManage.Utilities.V1.Constants;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using IManage.DomainServices.Errors;
using IManage.DomainServices.Enum;

namespace IManage.DomainServices.V1
{
    /// <summary>
    /// TokenService provides implemenation for ITokenService.
    /// </summary>
    public class TokenService : ITokenService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<TokenService> _localizer;
        private readonly ILogger<TokenService> _logger;
        private static readonly TaskFactory TaskFactory = new(CancellationToken.None, TaskCreationOptions.None, TaskContinuationOptions.None, TaskScheduler.Default);

        #endregion

        #region Constructor

        /// <summary>
        /// Initialises an instance of tokenservice.
        /// </summary>
        /// <param name="userRepository"><see cref="IUserRepository"></param>
        /// <param name="configuration"><see cref="IConfiguration"></param>
        /// <param name="localizer"><see cref="IStringLocalizer"></param>
        /// <param name="logger"><see cref="ILogger{TokenService}"></param>
        public TokenService(IUserRepository userRepository, IConfiguration configuration, IStringLocalizer<TokenService> localizer, ILogger<TokenService> logger)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _localizer = localizer;
            _logger = logger;
        }

        #endregion

        #region Public methods

        /// <summary>
        ///  Get access token based on the  Auth0 token
        /// </summary>
        /// <param name="subjectToken">Auth0 token.</param>
        /// <returns>Access token</returns>
        /// <exception cref="SubjectTokenInValidException">Thrown when Auth0 token either expired or invalid.</exception>
        /// <exception cref="IdpUserNotExistException">Thrown when Auth0 user not exist in Simit+.</exception>
        public Task<Token> GetAccessToken(string subjectToken)
        {
            string emailId = ValidateToken(subjectToken, out int tokenExpirationTime);
            var existingUser = _userRepository.GetUserByEmailId(emailId);


            if (existingUser == null)
            {
                _logger.LogError(TokenServiceConstants.UserNotFound);

                throw new IdpUserNotExistException(_localizer[TokenServiceConstants.UserNotFound].Value);
            }

            var functionRights = _userRepository.GetFunctionRightsByEmailId(emailId);

            if (functionRights == null)
            {
                _logger.LogError(TokenServiceConstants.FunctionRightsNotFound);

                throw new IdpUserFunctionRightNotExistException(_localizer[TokenServiceConstants.FunctionRightsNotFound].Value);
            }

            var userInfo = new User
            {
                Id = existingUser.Id,
                Name = existingUser.Name
            };

            string accessToken = GenerateJsonWebToken(userInfo, functionRights, tokenExpirationTime);
            var token = new Token { ExpirationTime = tokenExpirationTime, AccessToken = accessToken };

            return Task.FromResult(token);
        }

        /// <summary>
        ///  Validate subject token.
        /// </summary>
        /// <param name="subjectToken">Auth0 token.</param>
        /// <param name="tokenExpirationTime">Expiration time of the token.</param>
        /// <returns>emailId</returns>
        private string ValidateToken(string subjectToken, out int tokenExpirationTime)
        {
            const int maxExpirationTime = 30;
            string emailId = string.Empty;

            try
            {
                var domain = $"https://{_configuration[TokenServiceConstants.auth0Domain]}/";
                var cm = new ConfigurationManager<OpenIdConnectConfiguration>($"{domain.TrimEnd('/')}/.well-known/openid-configuration", new OpenIdConnectConfigurationRetriever());
                var openIdConfig = TaskFactory.StartNew(async () => await cm.GetConfigurationAsync()).Unwrap().GetAwaiter().GetResult();

                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(
                     subjectToken,
                     new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         ValidIssuer = domain,
                         IssuerSigningKeyResolver = (token, securityToken, kid, parameters) => new[] { openIdConfig.JsonWebKeySet.GetSigningKeys().FirstOrDefault(t => t.KeyId == kid) },
                         ValidateAudience = false,
                         ValidateLifetime = true,
                         ClockSkew = TimeSpan.Zero
                     }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var tokenExpiryTime = jwtToken.ValidTo;
                var span = tokenExpiryTime.Subtract(DateTime.UtcNow);
                tokenExpirationTime = span.Minutes > maxExpirationTime ? maxExpirationTime : span.Minutes;

                if (tokenExpirationTime == 0)
                {
                    throw new SecurityTokenExpiredException();
                }

                emailId = jwtToken.Claims.First(x => x.Type == "email").Value;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new SubjectTokenInValidException(_localizer[TokenServiceConstants.TokenExpired].Value, ex);
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new SubjectTokenInValidException(_localizer[TokenServiceConstants.TokenInvalid].Value, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new InternalServerException(_localizer[TokenServiceConstants.ErrorInDecodingSubjectToken].Value, ex);
            }

            return emailId;
        }

#endregion

        #region Private methods

        /// <summary>
        /// Generate JSON web token
        /// </summary>
        /// <param name="userInfo">User Details.</param>
        /// <param name="functionRights">Function rights of the user.</param>
        /// <param name="expirationTime">Expiration time for the toke.</param>
        /// <returns>Access token</returns>
        private string GenerateJsonWebToken(User userInfo, IList<string> functionRights, int expirationTime)
        {
            string issuer = _configuration[TokenServiceConstants.Issuer];
            var signingCredentials = GetSigningCredentials(SigningType.CertificateFromLocation);
            var now = DateTime.Now;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userInfo.Name),
                new Claim(TokenServiceConstants.UserID, userInfo.Id.ToString(new CultureInfo("en-US")))
            };
            foreach (var functionRight in functionRights)
            {
                claims.Add(new Claim(TokenServiceConstants.Roles, functionRight));
            }
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(new CultureInfo("en-US")), ClaimValueTypes.Integer64));

            try
            {
                var token = new JwtSecurityToken(issuer,
                            issuer,
                            claims.ToArray(),
                            expires: now.AddMinutes(expirationTime),
                            signingCredentials: signingCredentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new InternalServerException(_localizer[TokenServiceConstants.ErrorGeneratingJwt].Value, ex);
            }
        }

        /// <summary>
        /// Generate signing credentials
        /// </summary>
        /// <param name="signingType"></param>
        /// <returns><see cref="SigningCredentials"></returns>
        private SigningCredentials GetSigningCredentials(SigningType signingType)
        {
            SigningCredentials credentials;
            try
            {
                switch (signingType)
                {
                    case SigningType.CertificateFromLocation:
                        string resourceName = $"{TokenServiceConstants.EmbeddedResourceQualifier}.{TokenServiceConstants.ResourcePath}";
                        using (var certificateStream = typeof(TokenService).Assembly.GetManifestResourceStream(resourceName))
                        {
                            var rawBytes = new byte[certificateStream.Length];
                            var certificate = default(X509Certificate2);

                            while (certificateStream.Read(rawBytes, 0, Convert.ToInt32(certificateStream.Length)) > 0)
                            {
                                certificate = new X509Certificate2(rawBytes, "password", X509KeyStorageFlags.MachineKeySet);
                            }

                            credentials = new X509SigningCredentials(certificate);
                        }
                        break;

                    case SigningType.CertificateFromStore:
                        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                        store.Open(OpenFlags.ReadOnly | OpenFlags.IncludeArchived);
                        // Find by SubjectName
                        X509Certificate2Collection col = store.Certificates.Find(X509FindType.FindBySubjectName, _configuration[TokenServiceConstants.CertificateKey], true);
                        X509Certificate2 signingCert = col[0];
                        credentials = new X509SigningCredentials(signingCert);
                        break;

                    default:
                        RSA rsa = RSA.Create();
                        var privateKey = Convert.FromBase64String(_configuration[TokenServiceConstants.PrivateKey]);
                        rsa.ImportRSAPrivateKey(privateKey, out _);
                        credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSsaPssSha256)
                        {
                            CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                        };
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");

                throw new InternalServerException(_localizer[TokenServiceConstants.ErrorInSingningCredentials].Value, ex);
            }

            return credentials;
        }

        #endregion
    }
}
