using IManage.Domain.V1;
using IManage.ErrorHandling.ApiExceptions;
using IManage.Interfaces.V1.Repositories;
using IManage.Repositories.V1.DbContexts;
using IManage.Utilities.V1.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Infra = IManage.Repositories.V1.EFModels;

namespace IManage.Repositories.V1
{
    /// <summary>
    /// UserRepository Instance.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region Private Fields.

        private readonly IManageContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private readonly IStringLocalizer<UserRepository> _localizer;

        #endregion

        #region Constructor.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbContext"></param>
        public UserRepository(IManageContext dbContext, ILogger<UserRepository> logger, IStringLocalizer<UserRepository> localizer)
        {
            _dbContext = dbContext;
            _logger = logger;
            _localizer = localizer;
        }

        #endregion

        #region Public Methods.

        /// <summary>
        /// Gets all Users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllUsers()
        {
            var result = _dbContext.Users
                .Include(urm => urm.UserRoleMappings)
                .ThenInclude(r => r.Role)
                .ThenInclude(t => t.Type)
                .AsQueryable();

            return result.Select(u => new User
            {
                EmailId = u.EmailId,
                FullName = u.FullName,
                Id = u.Id,
                Name = u.Name,
                IsExfactory = (bool)u.IsExfactory,
                Picture = u.AvatarUrl
            }).AsQueryable();
        }

        /// <summary>
        /// Get roles of a user.
        /// </summary>
        /// <param name="id">Unique identifier of the user.</param>
        /// <returns>Roles</returns>
        public IQueryable<Role> GetUserRoles(int id)
        {
            return GetUserRoleInfo(id);
        }

        /// <summary>
        /// Get user info by email id.
        /// </summary>
        /// <param name="emailId">Email id.</param>
        /// <returns><see cref="User"/></returns>
        public User GetUserByEmailId(string emailId)
        {
            try
            {
                return GetDomainUser(_dbContext.Users.Where(u => u.EmailId == emailId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new InternalServerException(_localizer[CommonConstants.DatabaseException].Value, ex);
            }
        }

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">New user.</param>
        /// <param name="roleIds">Associated roles of the user.</param>
        /// <returns>User Id</returns>
        /// <exception cref="InternalServerException">Thrown when database exception occurs.</exception>
        public int AddUser(User user, int[] roleIds)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userRoleMappings = new List<V1.EFModels.UserRoleMapping>();

            var imanageUser = new V1.EFModels.User
            {
                EmailId = user.EmailId,
                Name = user.Name,
                FullName = user.FullName,
                AvatarUrl = user.Picture,
                IsExfactory = user.IsExfactory
            };
            using var transaction = new TransactionScope();

            try
            {
                _dbContext.Users.Add(imanageUser);
                _dbContext.SaveChanges();

                if (roleIds != null)
                {
                    foreach (var roleId in roleIds)
                    {
                        userRoleMappings.Add(new V1.EFModels.UserRoleMapping { UserId = imanageUser.Id, RoleId = roleId });
                    }
                }
                _dbContext.UserRoleMappings.AddRange(userRoleMappings);
                _dbContext.SaveChanges();
                transaction.Complete();
            }
            catch (Exception ex)
            {

                throw new InternalServerException("Database Exception", ex);
            }
            return imanageUser.Id;
        }

        /// <summary>
        /// Get all the functionrights based on emailId.
        /// </summary>
        /// <param name="emailId">Email Id.</param>
        /// <returns>List of functionrights.</returns>
        public IList<string> GetFunctionRightsByEmailId(string emailId)
        {
            try
            {
                var functionRights = (from u in _dbContext.Users
                                      join urm in _dbContext.UserRoleMappings on u.Id equals urm.UserId
                                      join r in _dbContext.Roles on urm.RoleId equals r.Id
                                      join rfm in _dbContext.RoleFunctionRightMappings on r.Id equals rfm.RoleId
                                      join fr in _dbContext.FunctionRights on rfm.FunctionRightId equals fr.Id
                                      where u.EmailId == emailId
                                      select fr.Name).ToList();
                return functionRights;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new InternalServerException(_localizer[CommonConstants.DatabaseException].Value, ex);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// User role info
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IQueryable<Role> GetUserRoleInfo(int userId)
        {
            try
            {
                return _dbContext.UserRoleMappings.Where(x => x.UserId == userId).Select(x => new Role
                {
                    Id = x.Role.Id,
                    Name = x.Role.Name,
                    Description = x.Role.Description,
                    IsExfactory = x.Role.IsExfactory,
                    Type = new RoleType { Id = x.Role.Type.Id, Name = x.Role.Type.Name },
                    FunctionRights = x.Role.RoleFunctionRightMappings.Select(r => new FunctionRight
                    {
                        Id = r.FunctionRightId,
                        Description = r.FunctionRight.Description,
                        Name = r.FunctionRight.Name,
                        Types = r.FunctionRight.FunctionRightTypeMappings.Select(frtm => new FunctionRightType { Id = frtm.Type.Id, Name = frtm.Type.Name }).ToList()
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message} - {ex.StackTrace}");
                throw new InternalServerException(_localizer[CommonConstants.DatabaseException].Value, ex);
            }
        }

        /// <summary>
        /// To convert the EFModel to Domain model.
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private User GetDomainUser(IQueryable<V1.EFModels.User> users)
        {
            return users.Select(x => new User
            {
                Id = x.Id,
                Name = x.Name,
                FullName = x.FullName,
                EmailId = x.EmailId,
                Picture = x.AvatarUrl,
                IsExfactory = x.IsExfactory.HasValue && x.IsExfactory.Value
            }).FirstOrDefault();
        }

        #endregion
    }
}
