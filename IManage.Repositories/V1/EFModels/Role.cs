namespace IManage.Repositories.V1.EFModels
{
    public partial class Role
    {
        public Role()
        {
            RoleFunctionRightMappings = new HashSet<RoleFunctionRightMapping>();
            UserRoleMappings = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public bool IsExfactory { get; set; }

        public virtual RoleType Type { get; set; }
        public virtual ICollection<RoleFunctionRightMapping> RoleFunctionRightMappings { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
    }
}