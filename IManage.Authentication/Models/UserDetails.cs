namespace IManage.Authentication.Models
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// Instance of UserDetails.
        /// </summary>
        public UserDetails()
        {
            FunctionRights = new List<string>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<string> FunctionRights { get; set; }
    }
}
