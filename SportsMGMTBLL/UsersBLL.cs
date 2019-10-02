
namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class UsersBLL
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
        //View The Entire Team Roster

        public List<Users>ViewMyTeam(int id)
        {
            List<Users> getUsers = new List<Users>();
            UsersDataAccess usersData = new UsersDataAccess();
            getUsers = usersData.ViewUsersOnTeam(id);

            return getUsers;
        }
        //Coaches can View Null Contracts
        public List<Users> ViewNullContracts()
        {
            //create a list of users without contracts to be assigned
            UsersDataAccess usersDataAccess = new UsersDataAccess();
            List<Users>viewNull=usersDataAccess.ViewNullContracts();
            return viewNull;
        }
        //Coaches can assign Contracts to unsigned players
        public bool AssignContracts(Users user)
        {
            //Tries to update a player contract returns true if succeeded.

            try
            {
                UsersDataAccess usersDataAccess = new UsersDataAccess();
                usersDataAccess.AssignContracts(user);
            }
            catch(Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
            }
            return true;
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
