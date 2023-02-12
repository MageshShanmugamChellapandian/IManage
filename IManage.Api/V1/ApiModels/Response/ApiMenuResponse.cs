namespace IManage.Api.V1.ApiModels.Response
{
    /// <summary>
    /// Api menu response model.
    /// </summary>
    public class ApiMenuResponse
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Menu name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Menu IconName
        /// </summary>
        public string? IconName { get; set; }

        /// <summary>
        /// Determines the default menu
        /// </summary>
        public bool? DefaultActive { get; set; }

        /// <summary>
        /// React component link url
        /// </summary>
        public string? LinkedTo { get; set; }
    }
}
