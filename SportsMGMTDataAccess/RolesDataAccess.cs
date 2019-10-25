using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Interfaces.IDataAccess;
using SportsMGMTCommon;



namespace SportsMGMTDataAccess
{
    public class RolesDataAccess:IRolesDataAccess
    {
        public string Connection = AppSettings.Default.ConnectionString;
       
        public List<Roles> GetRoles()
        {
            //make a list to store roles
            List<Roles> getRoles = new List<Roles>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewRoles", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Roles role = new Roles();
                                role.RoleID = (int)reader["roleID"];
                                role.RoleType = (string)reader["role_type"];
                                getRoles.Add(role);
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
            return getRoles;
        }
        //Method that can insert Roles
        public bool InsertRole(Roles role)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_addroles", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@type", role.RoleType);
                        con.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
            }
            return true;
        }
        //Method that checks User Access Rights
        public Roles CheckRoleAccess(Users user)
        {
            Roles getRoleAccess = new Roles();
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_checkroleaccessbyname", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@name", user.FullName);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["role_type"] != DBNull.Value)
                                {
                                    getRoleAccess.RoleType = (string)reader["role_type"];
                                }
                                else
                                {
                                    getRoleAccess.RoleType = "";
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                ExeceptionDataAccess exception = new ExeceptionDataAccess();
                exception.StoreExceptions(ex);
            }
            return getRoleAccess;
        }


        public bool DeleteRoles(string name)
        {
            //store the name of the Role into the Data Base
            List<Roles> getRoles = GetRoles();
            Roles role = getRoles.Find(m => m.RoleType == name);
            //Update the User with the Role and the Name provided
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteRole", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@id", role.RoleID);
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
            return true;

        }

    }

}

