
namespace SportsMGMTDataAccess
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    //Creates the connection to store dev exceptions
    public class ExeceptionDataAccess
        {
        //string Connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Itnitial Catalog=SportsMGMT-capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
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
                //Write the exception that couldn't be stored into a text file and throw
                File.WriteAllText("C:\\Users\\Admin-2\\source\\repos\\SportsMGMTApp\\SportsMGMTDataAccess\\Exceptions\\Unstored.txt", exe.Message);
                    return false;
                }
            return true;
            }
            //Make a method to view exceptions
            public List<ExceptionLog> GetExecptions()
            {
                List<ExceptionLog> exec = new List<ExceptionLog>();
                try
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_GetExceptions", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                            con.Open();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {

                                ExceptionLog exe = new ExceptionLog();
                                //transforms reader value to string even if its null
                                if (reader["message_recieved"]==DBNull.Value)
                                {
                                    exe.Message = "";
                                }
                                else
                                {
                                    //store message into the database as nvarchar if not null
                                    exe.Message = (string)reader["message_recieved"];
                                }
                                if (reader["date_logged"] == DBNull.Value)
                                {
                                    exe.DateLogged = DateTime.Today;
                                }
                                else
                                {
                                    exe.DateLogged = (DateTime)reader["date_logged"];
                                }
                                exec.Add(exe);
                                }
                            }
                        }
                    }

                }
                catch (Exception exception)
                {
                    StoreExceptions(exception);
                }
                return exec;
            }
            //Make a method to view exceptions by id
            public List<ExceptionLog> GetExceptionbyId(int id)
            {
                List<ExceptionLog> _returnListOfExceptions = new List<ExceptionLog>();
                try
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_GetExceptionsById", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                            command.Parameters.AddWithValue("@id", id);

                            con.Open();

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                ExceptionLog exe = new ExceptionLog();
                                //transforms reader value to string even if its null
                                if (reader["message_recieved"] == DBNull.Value)
                                {
                                    exe.Message = "";
                                }
                                else
                                {
                                    //store message into the database as nvarchar if not null
                                    exe.Message = (string)reader["message_recieved"];
                                }
                                if (reader["date_logged"] == DBNull.Value)
                                {
                                    exe.DateLogged = DateTime.Today;
                                }
                                else
                                {
                                    exe.DateLogged = (DateTime)reader["date_logged"];
                                }

                                _returnListOfExceptions.Add(exe);
                                }
                            }
                        }
                    }

                }
                catch (Exception exception)
                {
                    StoreExceptions(exception);
                }
                return _returnListOfExceptions;
            }
        }
    }


