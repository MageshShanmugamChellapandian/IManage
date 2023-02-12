using IManage.Api.V1.ApiModels.Response;
using IManage.Interfaces.V1.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.ComponentModel.Design;

namespace IManage.Api.V1.Controllers
{
    /// <summary>
    /// Infocontroller instance.
    /// </summary>
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Tags("Project")]
    [ApiVersion("1.0")]
    public class MenuController : ControllerBase
    {
        #region Private fields

        private readonly ILogger<MenuController> _logger;
        private readonly IStringLocalizer<MenuController> _localizer;
        private readonly IMenuService _menuService;

        #endregion

        #region Constructors

        /// <summary>
        /// Info controller constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        /// <param name="menuService"></param>
        public MenuController(ILogger<MenuController> logger, IStringLocalizer<MenuController> localizer, IMenuService menuService)
        {
            _logger = logger;
            _localizer = localizer;
            _menuService = menuService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets menus for a particular project.
        /// </summary> 
        /// <param name="projectId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>File and Api Versions.</returns>
        /// <response code="200">Menus of a project.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("project/{projectId}/menu")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMenus(int projectId, CancellationToken cancellationToken)
        {
            _logger.LogInformation(_localizer["ResourceChecking"].Value);
            var response = await _menuService.GetMenus(projectId);
            return Ok(response.Select(m => new ApiMenuResponse
            {
                DefaultActive = m.DefaultActive,
                IconName = m.IconName,
                Id = m.Id,
                LinkedTo = m.LinkedTo,
                Name = m.Name
            }).ToList());
        }

        #endregion
    }
}
