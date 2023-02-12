using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.DomainServices.Enum
{
    /// <summary>
    /// Enum for SigningType.
    /// </summary>
    public enum SigningType
    {
        /// <summary>
        /// Get the Certificate from some path. 
        /// </summary>
        CertificateFromLocation = 1,
        /// <summary>
        /// Get the Certicate from Store.
        /// </summary>
        CertificateFromStore = 2,

        /// <summary>
        /// Private and public key. 
        /// </summary>
        SignByKeys = 3
    }
}
