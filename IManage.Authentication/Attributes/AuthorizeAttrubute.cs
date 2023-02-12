
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using IManage.Authentication.Models;
using Microsoft.Extensions.Configuration;
using IManage.ErrorHandling.ApiExceptions;
using IManage.Authentication.Enum;

namespace IManage.Authentication.Attributes
{
    /// <summary>
    /// Custom Authorize attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        #region Fields

        /// <summary>
        /// Array of functionrights.
        /// </summary>
        private readonly string[] _apiFunctionRights;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="functionRights">Function rights</param>
        public AuthorizeAttribute(params string[] functionRights)
        {
            _apiFunctionRights = functionRights;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This method is used to authorize the incoming request.
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            const string algorithm = "RS256";
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var services = context.HttpContext.RequestServices;
            var configuration = services.GetService<IConfiguration>();
            var stringLocalizer = services.GetService<IStringLocalizer<AuthorizeAttribute>>();
            var user = (UserDetails)context.HttpContext.Items[AuthConstant.User];
            if (user == null)
            {
                var unauthorizedRes = new
                {
                    headerType = stringLocalizer[AuthorizeConstant.BearerToken].Value,
                    algorithm,
                    loginUrl = configuration[AuthorizeConstant.LoginKey]
                };
                var tokenException = context.HttpContext.Items[AuthConstant.TokenStatus] == null ? TokenException.SecurityTokenInvalidException
                                                                                                  : (TokenException)context.HttpContext.Items[AuthConstant.TokenStatus];

                string title = tokenException == TokenException.SecurityTokenExpiredException
                    ? stringLocalizer[AuthorizeConstant.TokenExpired].Value
                    : stringLocalizer[AuthorizeConstant.UnAuthorized].Value;
                throw new UnauthorizedException(title, System.Text.Json.JsonSerializer.Serialize(unauthorizedRes));
            }

            if (!IsAuthorized(user.FunctionRights))
            {
                throw new ForbiddenException(stringLocalizer[AuthorizeConstant.Forbidden].Value);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Check whether user is authorized or not.
        /// </summary>
        /// <param name="userFunctionRights"></param>
        /// <returns>Whether user is authorized or not</returns>
        private bool IsAuthorized(IEnumerable<string> userFunctionRights)
        {
            if (_apiFunctionRights != null && _apiFunctionRights.Length > 0)
            {
                return userFunctionRights.Any(f => _apiFunctionRights.Any(x => x == f));
            }
            else if (_apiFunctionRights != null && _apiFunctionRights.Length == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// Constants used for the resource key strings.
    /// </summary>
    public static class AuthorizeConstant
    {
        /// <summary>
        /// Forbidden
        /// </summary>
        public static string Forbidden { get; } = "Forbidden";

        /// <summary>
        /// Unauthorized
        /// </summary>
        public static string UnAuthorized { get; } = "Unauthorized";

        /// <summary>
        /// Token expired
        /// </summary>
        public static string TokenExpired { get; } = "Token expired";

        /// <summary>
        /// Bearer token
        /// </summary>
        public static string BearerToken { get; } = "Bearer token";

        /// <summary>
        /// AuthConfig:LoginUrl
        /// </summary>
        public static string LoginKey { get; } = "AuthConfig:LoginUrl";

        /// <summary>
        /// User
        /// </summary>
        public static string User { get; } = "User";

    }
}
