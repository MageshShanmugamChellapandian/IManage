using FluentValidation.AspNetCore;
using IManage.Api.V1.ApiModels.Request;
using IManage.Api.V1.ApiModels.Response;
using IManage.Interfaces.V1.Services;
using IManage.Utilities.V1.Constants.Controllers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IManage.Api.V1.Controllers
{
    /// <summary>
    /// Token controller instance.
    /// </summary>
    [Route("token")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ApiVersion("1.0")]
    public class TokenController : ControllerBase
    {
        #region Fields

        private readonly ITokenService _tokenService;
        private readonly ILogger<TokenController> _logger;
        private readonly IStringLocalizer<TokenController> _localizer;

        #endregion

        #region Constructor
        /// <summary>
        /// Initialises an instance for token controller
        /// </summary>
        /// <param name="tokenService"><see cref="ITokenService"/></param>
        /// <param name="logger"><see cref="ILogger{TokenController}"/></param>
        /// <param name="localizer"><see cref="IStringLocalizer{TokenController}"/></param>
        public TokenController(ITokenService tokenService, ILogger<TokenController> logger, IStringLocalizer<TokenController> localizer)
        {
            _tokenService = tokenService;
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Get access token. 
        /// </summary>
        /// <param name="req"></param>
        /// <returns>A new access token.</returns>
        /// <response code="200">Get Access token.</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetToken([CustomizeValidator(RuleSet = "New_Token")][FromForm] ApiTokenExchangeReq req)
        {
            _logger.LogInformation(TokenControllerConstants.GetToken);

            if (req == null)
            {
                _logger.LogError(nameof(req));

                throw new ArgumentNullException(nameof(req));
            }

            var token = await _tokenService.GetAccessToken(req.SubjectToken);
            var result = ApiTokenExchangeRes.Convert(token.AccessToken, token.ExpirationTime);

            return Ok(result);
        }

        #endregion
    }
}
