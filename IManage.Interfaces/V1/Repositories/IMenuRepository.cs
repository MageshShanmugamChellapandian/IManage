using IManage.Domain.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Repositories
{
    /// <summary>
    /// IMenuRepository interface.
    /// </summary>
    public interface IMenuRepository
    {
        /// <summary>
        /// Gets menus of the project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Task<IList<Menu>> GetMenus(int projectId);
    }
}
