using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IManage.Authentication
{
    /// <summary>
    /// IManage permissions.
    /// </summary>
    public class IManageFunctionRights
    {

        public const string AddUser = "im.user.add";

        public const string EditIMUser = "im.user.edit";

        public const string ViewIMUsers = "im.user.view";

        public const string ManageIMUsers = "im.user.manage";

        public const string DeleteIMUser = "im.user.delete";

        public const string AddNewRole = "im.role.add";

        public const string EditIMRole = "im.role.edit";

        public const string ViewIMRoles = "im.role.view";

        public const string DeleteRole = "im.role.delete";

        public const string ViewIMFunctionRights = "im.functionrights.view";

        public const string AddFunctionRights = "im.functionrights.add";

        public const string EditFunctionRights = "im.functionrights.edit";
    }
}
