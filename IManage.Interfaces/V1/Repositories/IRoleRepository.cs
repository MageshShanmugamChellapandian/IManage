using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Repositories
{
    /// <summary>
    /// RoleRepository interface.
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// Verifies whether array of roleIds exists or not
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns>true if all roleIds exists</returns>
        /// <exception cref="InternalServerException">Thrown when there is any database error occurs.</exception>
        Task<bool> IsRoleIdsExistsInDB(int[] roleIds);
    }
}
