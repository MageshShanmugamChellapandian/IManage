using IManage.Domain.V1;
using IManage.ErrorHandling.ApiExceptions;
using IManage.Interfaces.V1.Repositories;
using IManage.Interfaces.V1.Services;
using IManage.Utilities.V1.Constants;
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
    /// User service instance.
    /// </summary>
    public class UserService : IUserService
    {
        #region Private Fields.

        private readonly IUserRepository _userRepository;
        private readonly IStringLocalizer<UserService> _localizer;
        private readonly ILogger<UserService> _logger;
        private readonly IRoleRepository _roleRepository;

        #endregion

        #region Constructor.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="localizer"></param>
        /// <param name="logger"></param>
        /// <param name="roleRepository"></param>
        public UserService(IUserRepository userRepository, IStringLocalizer<UserService> localizer, ILogger<UserService> logger,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _localizer = localizer;
            _logger = logger;
            _roleRepository = roleRepository;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers().ToList();
        }
        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">New user.</param>
        /// <param name="roleIds">Associated roles of the user.</param>
        /// <returns>User</returns>
        /// <exception cref="DomainElementAlreadyExistsException">Thrown when user with same umcID alraedy present.</exception>
        public async Task<User> AddUser(User user, int[] roleIds)
        {
            if (user == null)
            {
                _logger.LogError(_localizer[UserServiceConstants.UserIsNUll].Value);

                throw new ArgumentNullException(_localizer[UserServiceConstants.UserIsNUll].Value);
            }

            var existingUser = _userRepository.GetUserByEmailId(user.EmailId);

            if (existingUser != null)
            {
                _logger.LogError(UserServiceConstants.ObjectAlreadyExist);

                throw new DomainElementAlreadyExistsException(_localizer[UserServiceConstants.ObjectAlreadyExist].Value);
            }

            int userId;

            if (roleIds.Any() && !await _roleRepository.IsRoleIdsExistsInDB(roleIds))
            {
                throw new NotFoundException(_localizer[UserServiceConstants.RoleNotFound].Value);
            }
            userId = _userRepository.AddUser(user, roleIds);

            return new User
            {
                Id = userId,
                Name = user.Name,
                FullName = user.FullName,
                IsExfactory = false,
                EmailId = user.EmailId,
                Picture = user.Picture,
                Roles = _userRepository.GetUserRoles(userId).ToList()
            };
        }

    }

    #endregion
}
