using IManage.Api.V1.ApiModels.Request;
using IManage.Api.V1.ApiModels.Response;
using IManage.Interfaces.V1.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace IManage.Api.V1.Controllers
{
    /// <summary>
    /// Project controller instance.
    /// </summary>
    [Route("project")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ApiVersion("1.0")]
    public class ProjectController : ControllerBase
    {
        #region Private fields.

        private readonly ILogger<ProjectController> _logger;
        private readonly IStringLocalizer<ProjectController> _localizer;
        private readonly IProjectService _projectService;

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        /// <param name="projectService"></param>
        public ProjectController(ILogger<ProjectController> logger, IStringLocalizer<ProjectController> localizer, IProjectService projectService)
        {
            _logger = logger;
            _localizer = localizer;
            _projectService = projectService;
        }

        /// <summary>
        /// Gets all project.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <response code="200">All available users.</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server exception</response>
        /// <returns>All projects.</returns>
        [HttpGet]
        //[Authorize(IManageFunctionRights.AddUser)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProjects(CancellationToken cancellationToken)
        {
            return Ok(new ApiProjectRes().ConvertToList((await _projectService.GetAllProjects()).ToList()));
        }

        /// <summary>
        /// Creates new Project.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST /project
        ///     {
        ///         "name": "New project",
        ///         "description": "New description",
        ///         "createdBy": "Magesh",
        ///         "lastModifiedBy": "Magesh"
        ///     }
        ///     
        /// </remarks>
        /// <param name="project"></param>
        /// <param name="cancellationToken"></param>
        /// <response code="201">Created</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal server exception</response>
        /// <returns>Newly created project.</returns>

        [HttpPost]
        //[Authorize(IManageFunctionRights.AddUser)]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CraeteProject(ApiProjectReq project, CancellationToken cancellationToken)
        {
            var createResource = await _projectService.CreateProject(project.ConvertToDomainObject(project));
            return Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.Path.Value}/{createResource.Id}", new ApiProjectRes().Convert(createResource));
        }
    }
}
