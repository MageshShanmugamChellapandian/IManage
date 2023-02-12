using IManage.Api.V1.ApiModels.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using IManage.Authentication.Attributes;

namespace IManage.Api.V1.Controllers
{
    /// <summary>
    /// Infocontroller instance.
    /// </summary>
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Route("info")]
    [ApiVersion("1.0")]
    public class InfoController : ControllerBase
    {
        #region Private fields

        private readonly ILogger<InfoController> _logger;
        private readonly IStringLocalizer<InfoController> _localizer;

        #endregion

        #region Constructors

        /// <summary>
        /// Info controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public InfoController(ILogger<InfoController> logger, IStringLocalizer<InfoController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets info of the api
        /// </summary> 
        /// <param name="cancellationToken"></param>
        /// <returns>File and Api Versions.</returns>
        /// <response code="200">File and Api versions.</response>
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetInfo(CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer["ResourceChecking"].Value);
            return Ok(new ApiInfoResponse { ApiVersion = "0.0.1", FileVersion = "0.0.1" });
        }

        #endregion
    }
}
