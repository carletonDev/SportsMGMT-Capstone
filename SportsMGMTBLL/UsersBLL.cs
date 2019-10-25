
namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class UsersBLL:IUser
    {
        //Method to Insert a new user
        public bool InsertNewUser(Users user)
        {

            UsersDataAccess userDA = new UsersDataAccess();
    


                try
                {
                    userDA.InsertUser(user);
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
            UsersDataAccess usersData = new UsersDataAccess();
            Users = usersData.GetUsers();
            return Users; 
        }
        //Method to Get a User by password for naming purposes
        public Users GetUsersByUserName(string username)
        {
            Users Users = new  Users();
            UsersDataAccess usersData = new UsersDataAccess();

            Users = usersData.GetUsersByUserName(username);
            return Users;
        }
       
        //Update Users
        public bool UpdateUser(Users user)
        {
            try
            {
                UsersDataAccess usersDataAccess = new UsersDataAccess();
                usersDataAccess.UpdateUser(user);
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
            }
            return true;
        }
        //Delete User By Name
        public bool DeleteUserByName(Users user)
        {
            try
            {
                UsersDataAccess usersDataAccess = new UsersDataAccess();
                usersDataAccess.DeleteUserByName(user);
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
            }
            return true;
        }
    }
}
