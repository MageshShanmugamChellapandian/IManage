using IManage.Domain.V1;
using IManage.Interfaces.V1.Repositories;
using IManage.Interfaces.V1.Services;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.DomainServices.V1
{
    /// <summary>
    /// Project service instance.
    /// </summary>
    public class ProjectService : IProjectService
    {
        #region Private fields.

        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectService> _logger;
        private readonly IStringLocalizer<ProjectService> _localizer;

        #endregion

        #region Constructors

        /// <summary>
        /// Project service constructor.
        /// </summary>
        /// <param name="projectRepository"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger, IStringLocalizer<ProjectService> localizer)
        {
            _projectRepository = projectRepository;
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all the project.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Project>> GetAllProjects ()
        {
            return await _projectRepository.GetAllProjects();
        }

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<Project> CreateProject(Project project)
        {
            return await _projectRepository.CreateProject(project);
        }

        #endregion

        #region Private methods

        #endregion
    }
}
