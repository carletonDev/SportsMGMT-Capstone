using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IUser
    {
       
        bool InsertNewUser(Users insert);
        List<Users> GetUsers();
        Users GetUsersByUserName(string username);
        bool UpdateUser(Users user);
        bool DeleteUserByName(Users user);
    }
}
