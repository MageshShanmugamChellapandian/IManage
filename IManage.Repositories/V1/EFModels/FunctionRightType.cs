namespace IManage.Repositories.V1.EFModels
{
    public partial class FunctionRightType
    {
        public FunctionRightType()
        {
            FunctionRightTypeMappings = new HashSet<FunctionRightTypeMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<FunctionRightTypeMapping> FunctionRightTypeMappings { get; set; }
    }
}