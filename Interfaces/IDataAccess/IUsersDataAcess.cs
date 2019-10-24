using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IUsersDataAcesss
    {
        void InsertUser(Users insert);
        List<Users> GetUsers();
        Users GetUsersByUserName(string username);
        bool UpdateUser(Users user);
        void DeleteUserByName(Users user);
    }
}
