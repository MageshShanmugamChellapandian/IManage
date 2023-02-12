namespace IManage.Domain.V1
{
    /// <summary>
    /// Represents the FunctionRight.
    /// </summary>
    public class FunctionRight
    {
        /// <summary>
        /// Gets or sets the unique identifier for the function right.
        /// </summary>
        /// <value>The functionrights's unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the functionrights's name.
        /// </summary>
        /// <value>The functionrights's name .</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the functionright.
        /// </summary>
        /// <value>The functionrights's description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the associated functionright types.
        /// </summary>
        /// <value>The associated function right types.</value>
        public IReadOnlyList<FunctionRightType> Types { get; set; }
    }
}