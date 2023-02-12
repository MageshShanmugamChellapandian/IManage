using IManage.Domain.V1;

namespace IManage.Api.V1.ApiModels.Response
{
    /// <summary>
    /// Project reponse model.
    /// </summary>
    public class ApiProjectRes
    {
        /// <summary>
        /// Unique Identidfier
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// Name of the project
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Description of the project.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// User who created the project.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// User who last modified the project.
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Datetime of the creation of the project
        /// </summary>
        public DateTime? CreatedAt { get; set; }

        /// <summary>
        /// Datetime id the last modification of the project
        /// </summary>
        public DateTime? LastModifiedAt { get; set; }
        
        /// <summary>
        /// Converts Domain model to Api response model.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ApiProjectRes Convert(Project project)
        {
            return project == null ? new ApiProjectRes() : new ApiProjectRes
            {
                CreatedAt= project.CreatedAt,
                CreatedBy= project.CreatedBy,
                LastModifiedBy= project.LastModifiedBy,
                Description= project.Description,
                Id= project.Id,
                Name= project.Name,
                LastModifiedAt = project.LastNodifiedAt
            };
        }

        /// <summary>
        /// Converts list of domain model to list of API model.
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        public List<ApiProjectRes> ConvertToList(List<Project> projects)
        {
            var response = new List<ApiProjectRes>();
            projects.ForEach(project =>
            {
                var newProject = new ApiProjectRes
                {
                    CreatedAt = project.CreatedAt,
                    CreatedBy = project.CreatedBy,
                    LastModifiedBy = project.LastModifiedBy,
                    Description = project.Description,
                    Id = project.Id,
                    Name = project.Name,
                    LastModifiedAt = project.LastNodifiedAt
                };
                response.Add(newProject);
            });
            return response;
        }
    }
}
