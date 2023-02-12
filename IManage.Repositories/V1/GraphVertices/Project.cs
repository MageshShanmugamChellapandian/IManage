namespace IManage.Repositories.V1.GraphVertices
{
    /// <summary>
    /// Project model.
    /// </summary>
    public class Project
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }

    }
}
