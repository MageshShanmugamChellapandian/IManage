using Microsoft.AspNetCore.Mvc;

namespace IManage.Api.V1.ApiModels.Request
{
    /// <summary>
    ///  Token Request Model.
    /// </summary>
    public class ApiTokenExchangeReq
    {
        #region Fields

        /// <summary>
        /// The grant type - urn:ietf:params:oauth:grant-type:token-exchange
        /// </summary> 
        [BindProperty(Name = "grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// The IDP token.
        /// </summary>
        [BindProperty(Name = "subject_token")]
        public string SubjectToken { get; set; }

        /// <summary>
        /// The subject token type - urn:ietf:params:oauth:token-type:access_token
        /// </summary>
        [BindProperty(Name = "subject_token_type")]
        public string SubjectTokenType { get; set; }

        #endregion
    }
}
