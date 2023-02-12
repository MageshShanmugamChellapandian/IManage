using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Domain.V1
{
    /// <summary>
    /// Represents the RoleType.
    /// </summary>
    public class RoleType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the roletype.
        /// </summary>
        /// <value>The roletype's unique identifier.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the roletype name .
        /// </summary>
        /// <value>The roletype name .</value>
        public string Name { get; set; }
    }
}
