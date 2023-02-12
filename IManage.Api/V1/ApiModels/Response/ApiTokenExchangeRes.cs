using System.Text.Json.Serialization;

namespace IManage.Api.V1.ApiModels.Response
{
    /// <summary>
    /// Used to create response for GetAccessToken request.
    /// </summary>
    public class ApiTokenExchangeRes
    {
        #region Fields

        /// <summary>
        /// Represents the JWT token.
        /// </summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Expiration time of the token.
        /// </summary>
        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Represents the type of the issued token.
        /// </summary>
        [JsonPropertyName("issuedTokenType")]
        public string IssuedTokenType { get; set; } = "urn:ietf:params:oauth:token-type:access_token";


        /// <summary>
        /// Represents type of token.
        /// </summary>
        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; } = "Bearer";

        #endregion

        #region Public methods

        /// <summary>
        /// Creates token exchange response.
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="expirationTime"></param>
        /// <returns><see cref="ApiTokenExchangeRes" /></returns>
        public static ApiTokenExchangeRes Convert(string accessToken, int expirationTime)
        {
            const int secondsMultplier = 60;
            return new ApiTokenExchangeRes
            {
                AccessToken = accessToken,
                ExpiresIn = expirationTime * secondsMultplier
            };
        }

        #endregion
    }
}
