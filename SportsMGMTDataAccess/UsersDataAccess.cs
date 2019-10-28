namespace SportsMGMTDataAccess
{
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    public class UsersDataAccess:IUsersDataAcesss
    {//CRUD users Data Access

        public string Connection = AppSettings.Default.ConnectionString;
        //Method that adds users to the Database without team
        //Create Users
        public void InsertUser(Users insert)
        {
                insert.FullName = insert.FirstName + " " + insert.LastName;
                try
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_userRegister", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                            command.Parameters.AddWithValue("@fullname", insert.FullName.ToString());
                            command.Parameters.AddWithValue("@address", insert.Address.ToString());
                            command.Parameters.AddWithValue("@email", insert.Email.ToString());
                            command.Parameters.AddWithValue("@phone", insert.Phone.ToString());
                            command.Parameters.AddWithValue("@username", insert.UserName.ToString());
                            command.Parameters.AddWithValue("@password", insert.Password.ToString());
   
                            con.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                }
                catch (Exception except)
                {
                    ExeceptionDataAccess ex = new ExeceptionDataAccess();
                    ex.StoreExceptions(except);
                }
            
        }
        //Method that retrieves all Users
        //Read Users
        public List<Users> GetUsers()
        {
            List<Users> getUsers = new List<Users>();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetUsers", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Add to objects send to list
                                Users users = new Users();
                                users.UserID = (int)reader["userID"];
                                users.FullName = reader["full_name"]!=DBNull.Value?(string)reader["full_name"]:Users.Null.FirstName;
                                users.Address = reader["address"] !=DBNull.Value?(string)reader["address"]:Users.Null.Address;
                                if (reader["email"] != DBNull.Value)
                                {
                                    users.Email = (string)reader["email"];
                                }
                                else { users.Email = Users.Null.Email; }
                                if (reader["phone"] != DBNull.Value)
                                {
                                    users.Phone = (string)reader["phone"];
                                }
                                else { users.Phone = Users.Null.Phone; }
                                if (reader["teamID_fk"] != DBNull.Value)
                                {
                                    users.TeamID = (int)reader["teamID_fk"];
                                }
                                else { users.TeamID = Users.Null.TeamID; }
                                if (reader["role_id"] != DBNull.Value)
                                {
                                    users.RoleID = (int)reader["role_id"];
                                }
                                //set to default null values if db returns null
                                else { users.RoleID = Users.Null.RoleID; }
                                if (reader["contractID_fk"] != DBNull.Value)
                                {
                                    users.ContractID = (int)reader["contractID_fk"];
                                }
                                else { users.ContractID = Users.Null.ContractID; }
                                if (reader["user_modified_by_fk"] != DBNull.Value)
                                {
                                    users.UserModified = (int)reader["user_modified_by_fk"];
                                }
                                else { users.UserModified = Users.Null.UserModified; }
                                if (reader["injury_status"] != DBNull.Value)
                                {
                                    users.InjuryStatus = (bool)reader["injury_status"];
                                }
                                else { users.InjuryStatus = Users.Null.InjuryStatus; }
                                if (reader["injurydesc"] != DBNull.Value)
                                {
                                    users.InjuryDescription = (string)reader["injurydesc"];
                                }
                                if (reader["username"] != DBNull.Value)
                                {
                                    users.UserName = (string)reader["username"];
                                }
                                else { users.UserName = Users.Null.UserName; }
                                if (reader["password"] != DBNull.Value)
                                {
                                    users.Password = (string)reader["password"];
                                }
                                else { users.Password = Users.Null.Password; }
                                if(reader["contract_duration"]!= DBNull.Value)
                                {
                                    users.ContractDuration = (int)reader["contract_duration"];
                                }
                                else { users.ContractDuration = Users.Null.ContractDuration; }
                                if(reader["contract_start"]!= DBNull.Value)
                                {
                                    users.ContractStart = (DateTime)reader["contract_start"];
                                }
                                else { users.ContractStart = Users.Null.ContractStart; }                                
                                getUsers.Add(users);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
            }

            return getUsers;
        }
        //Method that retrieves Users by Password
        public Users GetUsersByUserName(string username)
        {
            Users users = new Users();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetUserByUsername", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@username",username);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Add to object
                  
                                users.UserID = (int)reader["userID"];
                                users.FullName = reader["full_name"] != DBNull.Value ? (string)reader["full_name"] : Users.Null.FirstName;
                                users.Address = reader["address"] != DBNull.Value ? (string)reader["address"] : Users.Null.Address;
                                if (reader["email"] != DBNull.Value)
                                {
                                    users.Email = (string)reader["email"];
                                }
                                else { users.Email = Users.Null.Email; }
                                if (reader["phone"] != DBNull.Value)
                                {
                                    users.Phone = (string)reader["phone"];
                                }
                                else { users.Phone = Users.Null.Phone; }
                                if (reader["teamID_fk"] != DBNull.Value)
                                {
                                    users.TeamID = (int)reader["teamID_fk"];
                                }
                                else { users.TeamID = Users.Null.TeamID; }
                                if (reader["role_id"] != DBNull.Value)
                                {
                                    users.RoleID = (int)reader["role_id"];
                                }
                                //set to default null values if db returns null
                                else { users.RoleID = Users.Null.RoleID; }
                                if (reader["contractID_fk"] != DBNull.Value)
                                {
                                    users.ContractID = (int)reader["contractID_fk"];
                                }
                                else { users.ContractID = Users.Null.ContractID; }
                                if (reader["user_modified_by_fk"] != DBNull.Value)
                                {
                                    users.UserModified = (int)reader["user_modified_by_fk"];
                                }
                                else { users.UserModified = Users.Null.UserModified; }
                                if (reader["injury_status"] != DBNull.Value)
                                {
                                    users.InjuryStatus = (bool)reader["injury_status"];
                                }
                                else { users.InjuryStatus = Users.Null.InjuryStatus; }
                                if (reader["injurydesc"] != DBNull.Value)
                                {
                                    users.InjuryDescription = (string)reader["injurydesc"];
                                }
                                if (reader["username"] != DBNull.Value)
                                {
                                    users.UserName = (string)reader["username"];
                                }
                                else { users.UserName = Users.Null.UserName; }
                                if (reader["password"] != DBNull.Value)
                                {
                                    users.Password = (string)reader["password"];
                                }
                                else { users.Password = Users.Null.Password; }
                                if (reader["contract_duration"] != DBNull.Value)
                                {
                                    users.ContractDuration = (int)reader["contract_duration"];
                                }
                                else { users.ContractDuration = Users.Null.ContractDuration; }
                                if (reader["contract_start"] != DBNull.Value)
                                {
                                    users.ContractStart = (DateTime)reader["contract_start"];
                                }
                                else { users.ContractStart = Users.Null.ContractStart; }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
            }
            return users;
        }
        //Method that Updates allfields
        public bool UpdateUser(Users user)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("UpdateUsers", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 30;
                        command.Parameters.AddWithValue("@uid", user.UserID);
                        command.Parameters.AddWithValue("@tid",user.TeamID);
                        command.Parameters.AddWithValue("@cid", user.ContractID);
                        command.Parameters.AddWithValue("@full", user.FullName);
                        command.Parameters.AddWithValue("@usermod", user.UserModified);
                        command.Parameters.AddWithValue("@role", user.RoleID);
                        command.Parameters.AddWithValue("@address", user.Address);
                        if(user.Email is null)
                        {
                            command.Parameters.AddWithValue("@email", Users.Null.Email);
                        }
                        else { command.Parameters.AddWithValue("@email", user.Email); }

                        command.Parameters.AddWithValue("@phone", user.Phone);
                        command.Parameters.AddWithValue("@username", user.UserName);
                        command.Parameters.AddWithValue("@pass", user.Password);
                        command.Parameters.AddWithValue("@injury", user.InjuryStatus);
                        if(user.InjuryDescription is null)
                        {
                            command.Parameters.AddWithValue("@injurydesc", Users.Null.InjuryDescription);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@injurydesc", user.InjuryDescription);
                        }
                        command.Parameters.AddWithValue("@time", user.ContractDuration);
                        command.Parameters.AddWithValue("@start", user.ContractStart);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
                return false;

            }
            return true;
        }
        //Method that deletes players by name
        public void DeleteUserByName(Users user)
        {
            
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteByName", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@fullname", user.FullName);

                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);

            }
        }


    }
}
