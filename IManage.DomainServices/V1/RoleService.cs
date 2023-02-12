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
    /// Roleservice instance.
    /// </summary>
    public class RoleService : IRoleService
    {

        #region Private Fields

        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;
        private readonly IStringLocalizer<RoleService> _localizer;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public RoleService(IRoleRepository roleRepository, ILogger<RoleService> logger, IStringLocalizer<RoleService> localizer)
        {
            _roleRepository = roleRepository;
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

    }
}
