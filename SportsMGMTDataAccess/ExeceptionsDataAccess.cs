
namespace SportsMGMTDataAccess
{
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    //Creates the connection to store dev exceptions
    public class ExeceptionDataAccess:IExceptions
        {
        public string Connection = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        //Make a method to store exceptions into the database
        public bool StoreExceptions(Exception ex)
            {
                try
                {
                    //store into the database using my sp_LogException stored procedure
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_AddExceptions", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                        //if exception properties do not equal null store them otherwise leave as null
                        if (ex.Message != null)
                        {
                            command.Parameters.AddWithValue("@message", ex.Message);
                        }
                            command.Parameters.AddWithValue("@date", DateTime.Now);
                            con.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception exe)
                {
                File.WriteAllText("C:\\DoubleException.txt", exe.Message);
                    return false;
                }
            return true;
            }

        }
    }


