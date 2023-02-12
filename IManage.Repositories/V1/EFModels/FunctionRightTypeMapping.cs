namespace IManage.Repositories.V1.EFModels
{
    public partial class FunctionRightTypeMapping
    {
        public int FunctionrightId { get; set; }
        public int TypeId { get; set; }

        public virtual FunctionRight Functionright { get; set; }
        public virtual FunctionRightType Type { get; set; }
    }
}