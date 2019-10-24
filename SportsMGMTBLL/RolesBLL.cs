

namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public  class RolesBLL
    {
        //CRUD BLL for ROLES only Read actually implemented 
        //Populate a list of the roles
        public List<Roles> GetRoles()
        {
            RolesDataAccess rolesDataAccess = new RolesDataAccess();
            List<Roles> getRole = rolesDataAccess.GetRoles();
            return getRole;
        }            

        //check what a user's role is
        public Roles CheckRoleAccess(Users role)
        {
            Roles userAccess = new Roles();
            RolesDataAccess rolesDataAccess = new RolesDataAccess();
            userAccess=rolesDataAccess.CheckRoleAccess(role);

            return userAccess;
        }

        public void DeleteRoles(Roles role)
        {
            RolesDataAccess rolesDataAccess = new RolesDataAccess();
            rolesDataAccess.DeleteRoles(role.RoleType);
        }
    }
}
