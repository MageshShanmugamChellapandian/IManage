using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Domain.V1
{
    /// <summary>
    /// Represents the FunctionRightType.
    /// </summary>
    public class FunctionRightType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the functionright type.
        /// </summary>
        /// <value>The functionrighttype's unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name for functionright type name.
        /// </summary>
        /// <value>The functionrights's name.</value>
        public string Name { get; set; }
    }
}
