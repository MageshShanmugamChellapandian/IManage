using IManage.ErrorHandling.ApiExceptions;
using IManage.Interfaces.V1.Repositories;
using IManage.Repositories.V1.GraphVertices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Repositories.V1
{
    /// <summary>
    /// Project repository instance.
    /// </summary>
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        #region Private fields.

        private readonly ILogger<ProjectRepository> _logger;
        private readonly IStringLocalizer<ProjectRepository> _localizer;
        private readonly IDriver _neo4jDriver;
        private readonly IConfiguration _config;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        /// <param name="neo4jDriver"></param>
        /// <param name="config"></param>
        public ProjectRepository(ILogger<ProjectRepository> logger, IStringLocalizer<ProjectRepository> localizer, IDriver neo4jDriver, IConfiguration config)
            : base(neo4jDriver, logger, localizer,config)
        {
            _logger = logger;
            _localizer = localizer;
            _neo4jDriver = neo4jDriver;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all projects.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<Domain.V1.Project>> GetAllProjects()
        {
            try
            {
                StringBuilder queryString = new StringBuilder();
                queryString.Append("MATCH (proj:Project) RETURN ID(proj) as id, proj.Name as name, proj.Description as description, proj.CreatedBy as createdBy, proj.LastModifiedBy as lastModifiedBy, proj.CreatedAt as createdAt, proj.LastModifiedAt as lastModifiedAt");

                var parameters = new Dictionary<string, object>();

                var result = await ExecuteReadTransactionAndReturnRecordsAsync(queryString.ToString(), parameters);

                return result.Select(p=> new Domain.V1.Project
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    CreatedBy = p.CreatedBy,
                    Description = p.Description,
                    LastModifiedBy = p.LastModifiedBy,
                    LastNodifiedAt = p.LastModifiedAt,
                    Name = p.Name
                }).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, ex);
                throw new InternalServerException(_localizer["DatabaseException"].Value,ex);
            }

        }

        /// <summary>
        /// Creates new project.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InternalServerException"></exception>
        public async Task<Domain.V1.Project> CreateProject(Domain.V1.Project project)
        {
            if(project == null)
            {
                _logger.LogError(nameof(project));
                throw new ArgumentNullException(nameof(project));
            }
            try
            {
                StringBuilder queryString = new StringBuilder();
                queryString.Append("CREATE (proj: Project) \n");
                queryString.Append("SET proj.Name = $name \n");
                queryString.Append("SET proj.Description = $description \n");
                queryString.Append("SET proj.CreatedBy = $createdBy \n");
                queryString.Append("SET proj.LastModifiedBy = $lastModifiedBy \n");
                queryString.Append("SET proj.CreatedAt = $createdAt \n");
                queryString.Append("SET proj.LastModifiedAt = $lastModifiedAt \n");

                //queryString.Append("CREATE (menu:Menu) \n");
                //queryString.Append("SET menu.Name =$menuName \n");
                //queryString.Append("SET menu.IconName =$menuIconName \n");
                //queryString.Append("SET menu.DefaultActive =$menuDefaultActive \n");
                //queryString.Append("SET menu.LinkedTo =$menuLinkedTo \n");
                //queryString.Append("SET menu.Icon = @menuIcon \n");


                //queryString.Append("CREATE (proj)-[r:CONTAINS_MENU]->(menu) \n");


                queryString.Append("RETURN ID(proj) as id, \n");
                queryString.Append("proj.Name as name, \n");
                queryString.Append("proj.Description as description, \n");
                queryString.Append("proj.CreatedBy as createdBy, \n");
                queryString.Append("proj.LastModifiedBy as lastModifiedBy, \n");
                queryString.Append("proj.CreatedAt as createdAt, \n");
                queryString.Append("proj.LastModifiedAt as lastModifiedAt \n");



                var parameters = new Dictionary<string, object>()
                {
                    { "name", project.Name },
                    { "description", project.Description },
                    { "createdBy", project.CreatedBy },
                    { "lastModifiedBy", project.LastModifiedBy },
                    { "createdAt", project.CreatedAt },
                    { "lastModifiedAt", project.LastNodifiedAt }
                };

                var result = await ExecuteWriteTransactionAndReturnRecordAsync(queryString.ToString(), parameters);
                return new Domain.V1.Project 
                {
                    Name = result.Name,
                    Description = result.Description,
                    CreatedBy = result.CreatedBy,
                    LastModifiedBy = result.LastModifiedBy,
                    CreatedAt = result.CreatedAt,
                    Id= result.Id,
                    LastNodifiedAt = result.LastModifiedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, ex);
                throw new InternalServerException(_localizer["DatabaseException"].Value, ex);
            }
        }

        #endregion

        #region Private methods.


        #endregion

        #region Overridden methods

        /// <summary>
        /// Implementation of Abstract method.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected override Project ConvertToTypeSafe(IRecord record)
        {
            if(record == null)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, nameof(record));
                throw new ArgumentNullException(nameof(record));
            }

            return new Project
            {
                Id = record.Values["id"]?.As<string>(),
                Name = record.Values["name"]?.As<string>(),
                Description = record.Values["description"]?.As<string>(),
                CreatedBy = record.Values["createdBy"]?.As<string>(),
                LastModifiedBy = record.Values["lastModifiedBy"]?.As<string>(),
                CreatedAt = record.Values["createdAt"]?.As<DateTime>(),
                LastModifiedAt = record.Values["lastModifiedAt"]?.As<DateTime>(),
            };

        }

        #endregion
    }
}
