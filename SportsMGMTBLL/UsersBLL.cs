
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
    public class UsersBLL:IUser
    {
        IUsersDataAcesss usersDA;
        IExceptions ExceptionDA;

        public UsersBLL(IUsersDataAcesss user, IExceptions exe)
        {
            usersDA = user;
            ExceptionDA = exe;
        }
        //Method to Insert a new user
        public bool InsertNewUser(Users user)
        {


    


                try
                {
                    usersDA.InsertUser(user);
                }
                catch (Exception ex)
                {
                    ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                    ExceptionDA.StoreExceptions(ex);
                }
                return true;
            }
        //Method to Get All Users in Database
        public List<Users> GetUsers()
        {
            List<Users> Users = new List<Users>();
            UsersDataAccess usersDA = new UsersDataAccess();
            Users = usersDA.GetUsers();
            return Users; 
        }
        //Method to Get a User by password for naming purposes
        public Users GetUsersByUserName(string username)
        {
            UsersDataAccess usersDA = new UsersDataAccess();

            Users users = usersDA.GetUsersByUserName(username);
            return users;
        }
       
        //Update Users
        public bool UpdateUser(Users user)
        {
            try
            {
                usersDA.UpdateUser(user);
            }
            catch (Exception ex)
            {

                ExceptionDA.StoreExceptions(ex);
                return false;
            }
            return true;
        }
        //Delete User By Name
        public bool DeleteUserByName(Users user)
        {
            try
            {

                usersDA.DeleteUserByName(user);
            }
            catch (Exception ex)
            {

                ExceptionDA.StoreExceptions(ex);
            }
            return true;
        }
    }
}
