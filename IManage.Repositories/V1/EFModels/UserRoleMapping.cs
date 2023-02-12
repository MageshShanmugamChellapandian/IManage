namespace IManage.Repositories.V1.EFModels
{
    public partial class UserRoleMapping
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}