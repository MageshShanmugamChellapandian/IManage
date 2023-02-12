
namespace IManage.Domain.V1
{
    /// <summary>
    /// Represents the Role.
    /// </summary>
    public class Role
    {
        #region Constructor
        public Role()
        {
            FunctionRights = new List<FunctionRight>();
            Users = new List<User>();
        }

        #endregion

        #region Fields

        /// <summary>
        /// Gets or sets the unique identifier for the role.
        /// </summary>
        /// <value>The role's unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        /// <value>The role name .</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the role.
        /// </summary>
        /// <value>The role description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the associated roletype.
        /// </summary>
        /// <value>The associated roletype.</value>
        public RoleType Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the specified role is exfactoty.
        /// </summary>
        /// <value><c>True</c>. if role is exfactory ; otherwise, <c>false</c>.
        /// The default value is <c>false</c>.</value>
        public bool IsExfactory { get; set; }

        /// <summary>
        /// Gets or sets the associated function rights.
        /// </summary>
        /// <value>The associated function rights.</value>
        public IReadOnlyList<FunctionRight> FunctionRights { get; set; } // ?? Looks like old rule S4004 https://rules.sonarsource.com/csharp/RSPEC-4004 

        /// <summary>
        /// Gets or sets the associated users.
        /// </summary>
        /// <value>The associated function rights.</value>
        public IReadOnlyList<User> Users { get; set; }

        #endregion
    }
}
