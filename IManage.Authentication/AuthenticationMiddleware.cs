using IManage.Authentication.Enum;
using IManage.Authentication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;

namespace IManage.Authentication
{
    /// <summary>
    /// This middleware is used to authenticate the incoming requests.
    /// </summary>
    public class AuthenticationMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;

        #endregion

        #region Constructors/Destructors   

        /// <summary>
        /// Initializes an instance of AuthenticationMiddleware.
        /// </summary>
        /// <param name="next"></param>
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to authenticate the incoming request.
        /// </summary>
        /// <param name="context"></param>
        /// <returns>Next task that needs to be executed</returns>
        public async Task Invoke(HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var token = context.Request.Headers[AuthConstant.Authorization].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                SetContext(context, token);
            }

            await _next(context);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Initialize the context.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        private void SetContext(HttpContext context, string token)
        {
            const string id = "id";
            const string name = "name";
            const string role = "scope";
            const string certificatePath = @"Certificate\IManage.cer";
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                //certificate should be stored in certificate store

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,    

                    IssuerSigningKey = new X509SecurityKey(new X509Certificate2(AppContext.BaseDirectory + certificatePath)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                UserDetails user = new()
                {
                    Id = int.Parse(jwtToken.Claims.First(x => x.Type == id).Value, new CultureInfo("en-US")),
                    Name = jwtToken.Claims.First(x => x.Type == name).Value,
                    FunctionRights = jwtToken.Claims.Where(x => x.Type == role).Select(x => x.Value).ToList()
                };

                context.Items[AuthConstant.User] = user;
            }
            catch (ArgumentNullException)
            {
                context.Items[AuthConstant.TokenStatus] = TokenException.ArgumentNullException;
            }
            catch (ArgumentException)
            {
                context.Items[AuthConstant.TokenStatus] = TokenException.ArgumentException;
            }
            catch (SecurityTokenExpiredException)
            {
                context.Items[AuthConstant.TokenStatus] = TokenException.SecurityTokenExpiredException;
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                context.Items[AuthConstant.TokenStatus] = TokenException.SecurityTokenInvalidSignatureException;
            }
            catch (SecurityTokenInvalidSigningKeyException)
            {
                context.Items[AuthConstant.TokenStatus] = TokenException.SecurityTokenInvalidSigningKeyException;
            }

        }

        #endregion
    }

    /// <summary>
    /// Authorization Constant.
    /// </summary>
    public static class AuthConstant
    {
        public static string User { get; } = "user";
        public static string TokenStatus { get; } = "TokenStatus";
        public static string Authorization { get; } = "Authorization";
    }
}
