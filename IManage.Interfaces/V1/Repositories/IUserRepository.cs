using IManage.Domain.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Repositories
{
    /// <summary>
    /// User repository interface.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetAllUsers();

        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">New user.</param>
        /// <param name="roleIds">Associated roles of the user.</param>
        /// <returns>User Id</returns>
        /// <exception cref="InternalServerException">Thrown when database exception occurs.</exception>
        public int AddUser(User user, int[] roleIds);

        /// <summary>
        /// Get user info by email id.
        /// </summary>
        /// <param name="emailId">Email id.</param>
        /// <returns><see cref="User"/></returns>
        public User GetUserByEmailId(string emailId);

        /// <summary>
        /// Get roles of a user.
        /// </summary>
        /// <param name="id">Unique identifier of the user.</param>
        /// <returns>Roles</returns>
        public IQueryable<Role> GetUserRoles(int id);

        /// <summary>
        /// Get all the functionrights based on Email id.
        /// </summary>
        /// <param name="emailId">Email Id.</param>
        /// <returns>List of functionrights.</returns>
        public IList<string> GetFunctionRightsByEmailId(string emailId);
    }
}
