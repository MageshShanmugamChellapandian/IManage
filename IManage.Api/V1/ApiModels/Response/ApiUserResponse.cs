namespace IManage.Api.V1.ApiModels.Response
{
    /// <summary>
    /// ApiUserResponse model
    /// </summary>
    public class ApiUserResponse
    {
        #region Fields

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        /// <value>User unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name for the user.
        /// </summary>
        /// <value>The user name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the fullname .
        /// </summary>
        /// <value>The user fullname.</value>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the User email id.
        /// </summary>
        /// <value>The user email id.</value>
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the Picture.
        /// </summary>
        /// <value>Picture.</value>
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the specified user is exfactoty.
        /// </summary>
        /// <value><c>True</c>. if user is exfactory ; otherwise, <c>false</c>.
        /// The default value is <c>false</c>.</value>
        public bool IsExfactory { get; set; }

        #endregion
    }
}
