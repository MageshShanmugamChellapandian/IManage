using IManage.Domain.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Interfaces.V1.Repositories
{
    /// <summary>
    /// Project repository interface.
    /// </summary>
    public interface IProjectRepository
    {
        public Task<IList<Project>> GetAllProjects();

        public Task<Project> CreateProject(Project project);
    }
}
