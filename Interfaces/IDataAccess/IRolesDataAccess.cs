using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IRolesDataAccess
    {
        List<Roles> GetRoles();
        bool InsertRole(Roles role);
        Roles CheckRoleAccess(Users user);
        bool DeleteRoles(string name);
    }
}
