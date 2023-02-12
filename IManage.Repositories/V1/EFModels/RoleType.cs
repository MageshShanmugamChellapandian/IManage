namespace IManage.Repositories.V1.EFModels
{
    public partial class RoleType
    {
        public RoleType()
        {
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}