using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IRole
    {
        List<Roles> GetRoles();

        Roles CheckRoleAccess(Users user);
        void DeleteRoles(Roles role);
    }
}
