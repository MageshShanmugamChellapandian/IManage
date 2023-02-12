using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Utilities.V1.Constants
{
    /// <summary>
    /// Constants used for operations req for Token Exchange.
    /// </summary>
    public static class TokenServiceConstants
    {
        /// <summary>
        /// Certificate Path.
        /// </summary>
        public static readonly string ResourcePath = "Certificate.IManage.pfx";

        /// <summary>
        /// EmbeddedResourceQualifier
        /// </summary>
        public static readonly string EmbeddedResourceQualifier = "IManage.DomainServices";

        /// <summary>
        /// Auth0 Token not after claim name 
        /// </summary>
        public static readonly string NotAfter = "notafter";

        /// <summary>
        /// Auth0 Token user claim name 
        /// </summary>
        public static readonly string User = "user";

        /// <summary>
        /// Auth0 Token id property claim name 
        /// </summary>
        public static readonly string Id = "id";

        /// <summary>
        /// Auth0 Token name property claim name 
        /// </summary>
        public static readonly string Name = "name";

        /// <summary>
        /// Access Token issuer attribute 
        /// </summary>

        public static readonly string Issuer = "JWT:Issuer";

        /// <summary>
        /// Access Token private key attribute 
        /// </summary>
        public static readonly string PrivateKey = "JWT:PrivateKey";

        /// <summary>
        /// Access Token certificate key attribute 
        /// </summary>
        public static readonly string CertificateKey = "JWT:CertName";

        /// <summary>
        /// Auth0 domain
        /// </summary>
        public static readonly string auth0Domain = "IdpConfig:AuthDomain";

        /// <summary>
        /// Auth0 API identifier
        /// </summary>
        public static readonly string auth0ApiIdentifier = "Auth0Config:Auth0ApiIdentifier";

        /// <summary>
        /// Access Token User ID claim name 
        /// </summary>
        public static readonly string UserID = "id";

        /// <summary>
        /// Access Token Role claim name 
        /// </summary>
        public static readonly string Roles = "scope";

        /// <summary>
        /// User not found
        /// </summary>
        public static readonly string UserNotFound = "User not found";

        /// <summary>
        /// Token Invalid
        /// </summary>
        public static readonly string TokenInvalid = "Token Invalid";

        /// <summary>
        /// Token Invalid
        /// </summary>
        public static readonly string TokenExpired = "Token expired";

        /// <summary>
        /// FunctionRight not found.
        /// </summary>
        public static string FunctionRightsNotFound { get; } = "FunctionRight not found";

        /// <summary>
        /// Error in decoding subject token.
        /// </summary>
        public static string ErrorInDecodingSubjectToken { get; } = "Error in decoding subject token";

        /// <summary>
        /// Error in signing credentials.
        /// </summary>
        public static string ErrorInSingningCredentials { get; } = "Error in signing credentials";

        /// <summary>
        /// Error in generating jwt.
        /// </summary>
        public static string ErrorGeneratingJwt { get; } = "Error in generating jwt";
    }
}
