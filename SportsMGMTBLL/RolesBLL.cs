

namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public  class RolesBLL:IRole
    {
        //CRUD BLL for ROLES only Read actually implemented 
        //Populate a list of the roles
        IRolesDataAccess rolesDataAccess;
        public RolesBLL(IRolesDataAccess roles)
        {
            rolesDataAccess = roles;
        }
        public List<Roles> GetRoles()
        {

            List<Roles> getRole = rolesDataAccess.GetRoles();
            return getRole;
        }            

        //check what a user's role is
        public Roles CheckRoleAccess(Users role)
        {

           Roles userAccess=rolesDataAccess.CheckRoleAccess(role);
            return userAccess;
        }

        public void DeleteRoles(Roles role)
        {
            rolesDataAccess.DeleteRoles(role.RoleType);
        }
    }
}
