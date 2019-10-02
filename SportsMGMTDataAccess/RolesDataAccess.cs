using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsMGMTCommon;

namespace SportsMGMTDataAccess
{
    public class RolesDataAccess
    {
        //string Connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Itnitial Catalog=SportsMGMT-capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string Connection = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        //Method that retrieves all roles
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
        //Method that updates the roles and stores the user who last modified the record in the database
        public void UpdateRolesByName(Users user, Roles role)
        {
            //Update the User with the Role and the Name provided
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdateNullRolesByName", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@rid", role.RoleID);
                        command.Parameters.AddWithValue("@uid",user.UserID);
                        command.Parameters.AddWithValue("@name", user.FullName);
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

