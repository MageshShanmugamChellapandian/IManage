namespace IManage.Repositories.V1.EFModels
{
    public partial class FunctionRight
    {
        public FunctionRight()
        {
            FunctionRightTypeMappings = new HashSet<FunctionRightTypeMapping>();
            RoleFunctionRightMappings = new HashSet<RoleFunctionRightMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<FunctionRightTypeMapping> FunctionRightTypeMappings { get; set; }
        public virtual ICollection<RoleFunctionRightMapping> RoleFunctionRightMappings { get; set; }
    }
}