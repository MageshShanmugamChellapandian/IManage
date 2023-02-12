using IManage.Domain.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Services
{
    /// <summary>
    /// ITokenService is used to expose token operations.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        ///  Get access token based on the Auth0 token.
        /// </summary>
        /// <param name="subjectToken">Auth0 token.</param>
        /// <returns>Access token</returns>
        /// <exception cref="SubjectTokenInvalidException">Thrown when Auth0 token either expired or invalid.</exception>
        /// <exception cref="IdpUserNotExistException">Thrown when Idp user not exist in Simit+.</exception>
        public Task<Token> GetAccessToken(string subjectToken);
    }
}
