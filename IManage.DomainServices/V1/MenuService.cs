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
    /// MenuService
    /// </summary>
    public class MenuService : IMenuService
    {
        #region Private fields.

        private readonly ILogger<MenuService> _logger;
        private readonly IStringLocalizer<MenuService> _localizer;
        private readonly IMenuRepository _menuRepository;

        #endregion


        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        /// <param name="menuRepository"></param>
        public MenuService(ILogger<MenuService> logger, IStringLocalizer<MenuService> localizer, IMenuRepository menuRepository)
        {
            _localizer = localizer;
            _logger = logger;
            _menuRepository = menuRepository;
        }

        #endregion

        #region Public methods.

        /// <summary>
        /// Returns all the menus for the project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<Menu>> GetMenus(int projectId)
        {
            return await _menuRepository.GetMenus(projectId);
        }

        #endregion
    }
}
