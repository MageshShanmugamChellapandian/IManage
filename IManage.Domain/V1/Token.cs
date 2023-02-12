using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Domain.V1
{
    /// <summary>
    /// Represents the token.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Expiration time of token.
        /// </summary>
        public int ExpirationTime { get; set; }

        /// <summary>
        /// Access token
        /// </summary>
        public string AccessToken { get; set; }
    }
}
