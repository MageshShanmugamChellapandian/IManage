using IManage.Domain.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Services
{
    public interface IUserService
    {
        public IReadOnlyList<User> GetAllUsers();
        /// <summary>
        /// Add a user.
        /// </summary>
        /// <param name="user">New user.</param>
        /// <param name="roleIds">Associated roles of the user which can be null also.</param>
        /// <returns>User</returns>
        /// <exception cref="DomainElementAlreadyExistsException">Thrown when user with same emailId alraedy present.</exception>
        public Task<User> AddUser(User user, int[] roleIds);
    }
}
