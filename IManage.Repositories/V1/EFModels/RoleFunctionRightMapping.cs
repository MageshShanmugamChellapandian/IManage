namespace IManage.Repositories.V1.EFModels
{
    public partial class RoleFunctionRightMapping
    {
        public int RoleId { get; set; }
        public int FunctionRightId { get; set; }

        public virtual FunctionRight FunctionRight { get; set; }
        public virtual Role Role { get; set; }
    }
}