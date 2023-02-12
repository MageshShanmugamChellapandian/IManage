using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Utilities.V1.Constants
{
    /// <summary>
    /// Constants used for the resource key strings used in User Service.
    /// </summary>
    public static class UserServiceConstants
    {
        /// <summary>
        /// User not found.
        /// </summary>
        public static string UserNotFound { get; } = "User not found";

        /// <summary>
        /// User is null.
        /// </summary>
        public static string UserIsNUll { get; } = "User is null";

        /// <summary>
        /// Roleids are invalid
        /// </summary>
        public static string InvalidRoleIds { get; } = "Invalid roleids.";

        /// <summary>
        /// User is already associated with roleIds
        /// </summary>
        public static string UserIsAlreadyAssociated { get; } = "User is already associated with the role";


        /// <summary>
        /// Object already exist
        /// </summary>
        public static string ObjectAlreadyExist { get; } = "The same object already exists!";

        /// <summary>
        /// Can't delete ExFactory user
        /// </summary>
        public static string DeleteExFactoryUser { get; } = "Can't delete ExFactory user";

        /// <summary>
        /// Role not found.
        /// </summary>
        public static string RoleNotFound { get; } = "Role not found";

        /// <summary>
        /// Queue publish failed.
        /// </summary>
        public static string QueuePublishFailed { get; } = "QueueService publish failed";
    }
}
