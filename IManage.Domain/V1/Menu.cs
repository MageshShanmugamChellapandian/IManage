using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Domain.V1
{
    /// <summary>
    /// Menu Model.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Unique Identifier.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// Menu name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Menu IconName
        /// </summary>
        public string? IconName { get; set; }

        /// <summary>
        /// Determines the default menu
        /// </summary>
        public bool? DefaultActive { get; set; }

        /// <summary>
        /// React component link url
        /// </summary>
        public string? LinkedTo { get; set; }
    }
}
