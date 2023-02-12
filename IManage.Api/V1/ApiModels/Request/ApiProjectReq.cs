using IManage.Domain.V1;

namespace IManage.Api.V1.ApiModels.Request
{
    /// <summary>
    /// ApiProjectReq model
    /// </summary>
    public class ApiProjectReq
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
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Datetime id the last modification of the project
        /// </summary>
        public DateTime? LastModifiedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// Converts Api request model to domain model.
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Project ConvertToDomainObject(ApiProjectReq req)
        {
            return new Project
            {
                Id = req.Id,
                Name = req.Name,
                Description = req.Description,
                CreatedBy = req.CreatedBy,
                LastModifiedBy = req.LastModifiedBy,
                CreatedAt = req.CreatedAt,
                LastNodifiedAt = req.LastModifiedAt
            };
        }
    }
}
