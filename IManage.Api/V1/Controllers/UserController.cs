using IManage.Api.V1.ApiModels.Request;
using IManage.Api.V1.ApiModels.Response;
using IManage.Authentication;
using IManage.Authentication.Attributes;
using IManage.Interfaces.V1.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IManage.Api.V1.Controllers
{
    /// <summary>
    /// UserController instance.
    /// </summary>
    [Route("user")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        #region Private Fields

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IStringLocalizer<UserController> _localizer;

        #endregion

        #region Constructor
        /// <summary>
        /// User controller constructor.
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public UserController(IUserService userService, ILogger<UserController> logger, IStringLocalizer<UserController> localizer)
        {
            _userService = userService;
            _localizer = localizer;
            _logger = logger;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>All users.</returns>
        /// <response code="200">All available users.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server exception</response>
        [HttpGet]
        //[Authorize(IManageFunctionRights.AddUser)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public IList<ApiUserResponse> GetUsers(CancellationToken cancellationToken)
        {
            var result = _userService.GetAllUsers().Select(u=> new ApiUserResponse
            {
                EmailId = u.EmailId,
                FullName = u.FullName,
                Id = u.Id,
                IsExfactory = u.IsExfactory,
                Name = u.Name,
                Picture = u.Picture
            }).ToList();
            return result;
        }

        /// <summary>
        /// Creates new user.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Created user.</returns>
        /// <response code="201">Created user.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server exception</response>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult PostAsync(ApiUserRequest request)
        {
            return Created("", new ApiUserResponse());
        }

        #endregion
    }
}
