using IManage.ErrorHandling.ApiExceptions;
using IManage.Interfaces.V1.Repositories;
using IManage.Repositories.V1.DbContexts;
using IManage.Utilities.V1.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Repositories.V1
{
    /// <summary>
    /// Rolerepository instance.
    /// </summary>
    public class RoleRepository : IRoleRepository
    {
        #region Private Fields.

        private readonly ILogger<RoleRepository> _logger;
        private readonly IStringLocalizer<RoleRepository> _localizer;
        private readonly IManageContext _dbContext;

        #endregion

        #region Constructor.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        /// <param name="localizer"></param>
        public RoleRepository(IManageContext dbContext, ILogger<RoleRepository> logger, IStringLocalizer<RoleRepository> localizer)
        {
            _localizer= localizer;
            _logger = logger;
            _dbContext = dbContext;
        }

        #endregion

        #region Public methods.

        /// <summary>
        /// Get info on role if it exist in DB otherwise error by passing array of roleIds.
        /// </summary>
        /// <param name="roleIds">Unique identifier for the role.</param>
        /// <returns>Role.</returns>
        public async Task<bool> IsRoleIdsExistsInDB(int[] roleIds)
        {
            try
            {
                if (roleIds == null)
                {
                    _logger.LogError(RoleRepositoryConstants.RoleIdsNull);

                    throw new ArgumentNullException(nameof(roleIds));
                }

                foreach (var roleId in roleIds)
                {
                    if (!await _dbContext.Roles.AnyAsync(x => x.Id == roleId))
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");

                throw new InternalServerException(_localizer[CommonConstants.DatabaseException].Value, ex);
            }
        }
    }

    #endregion
}
