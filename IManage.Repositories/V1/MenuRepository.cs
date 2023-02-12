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
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        #region Private fields

        private readonly ILogger<MenuRepository> _logger;
        private readonly IStringLocalizer<MenuRepository> _localizer;
        private readonly IDriver _neo4jDriver;
        private readonly IConfiguration _config;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        /// <param name="neo4jDriver"></param>
        /// <param name="config"></param>
        public MenuRepository(ILogger<MenuRepository> logger, IStringLocalizer<MenuRepository> localizer, IDriver neo4jDriver, IConfiguration config)
            : base(driver:neo4jDriver,logger,localizer,config)
        {
            _logger = logger;
            _localizer = localizer;
            _neo4jDriver = neo4jDriver;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets menus of the project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<IList<Domain.V1.Menu>> GetMenus(int projectId)
        {
            try
            {
                var parameters = new Dictionary<string, object>();
                StringBuilder queryString = new StringBuilder();

                queryString.Append("MATCH(proj:Project) where ID(proj) = $projectId \n");
                queryString.Append("MATCH(menu:Menu)-[r:CONTAINS_MENU*]->(proj) \n");
                queryString.Append("RETURN ID(menu) as id, menu.Name as name, menu.IconName as iconName, menu.DefaultActive as defaultActive, menu.LinkedTo as linkedTo \n");

                parameters.Add("projectId", projectId);

                var result = await ExecuteReadTransactionAndReturnRecordsAsync(queryString.ToString(), parameters);

                return result.Select(m => new Domain.V1.Menu
                {
                    DefaultActive = m.DefaultActive,
                    IconName = m.IconName,
                    Id = m.Id,
                    LinkedTo = m.LinkedTo,
                    Name = m.Name
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, ex);
                throw new InternalServerException(_localizer["DatabaseException"].Value, ex);
            }
        }

        #endregion

        #region Overridden methods

        /// <summary>
        /// Implementation of Abstract method.
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        protected override Menu ConvertToTypeSafe(IRecord record)
        {
            if (record == null)
            {
                _logger.LogError(_localizer["DatabaseException"].Value, nameof(record));
                throw new ArgumentNullException(nameof(record));
            }

            return new Menu
            {
                Id = record.Values["id"]?.As<string>(),
                Name = record.Values["name"]?.As<string>(),
                DefaultActive = record.Values["defaultActive"]?.As<bool>(),
                LinkedTo = record.Values["linkedTo"]?.As<string>(),
                IconName = record.Values["iconName"]?.As<string>()
            };

        }

        #endregion
    }
}
