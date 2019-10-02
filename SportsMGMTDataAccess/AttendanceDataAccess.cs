
namespace SportsMGMTDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTCommon;
    //Creates the connection for the app attendance tables and bar chart
    public class AttendanceDataAccess
    {
        string Connection = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        //string Connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Itnitial Catalog=SportMGMT-capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //Get A List of users who attended or didn't attend games depending on what you need
        public List<Users> getGameAttendance(GameAttendance gameAttendance)
        {
            //make a list to store Games
            List<Users> getAttended = new List<Users>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewGameAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@attend", gameAttendance.Attended);
                        command.Parameters.AddWithValue("@id", gameAttendance.GameID);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Users user = new Users();
                                if (reader["full_name"] != DBNull.Value)
                                {
                                    user.FullName = (string)reader["full_name"];
                                }
                                if (reader["Phone"] != DBNull.Value)
                                {
                                    user.Phone = (string)reader["Phone"];
                                }
                                if (reader["Email"] != DBNull.Value)
                                {
                                    user.Email = (string)reader["Email"];
                                }
                                getAttended.Add(user);
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
            return getAttended;
        }
        //Get A List of User who attended or didnt attend practice depending on input provided
        public List<Users> getPracticeAttendance(PracticeAttended practiceAttendance)
        {
            //make a list to store Games
            List<Users> getAttended = new List<Users>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@attend", practiceAttendance.Attended);
                        command.Parameters.AddWithValue("@id", practiceAttendance.PracticeID);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Users user = new Users();
                                if (reader["full_name"] != DBNull.Value)
                                {
                                    user.FullName = (string)reader["full_name"];
                                }
                                if (reader["Phone"] != DBNull.Value)
                                {
                                    user.Phone = (string)reader["Phone"];
                                }
                                if (reader["Email"] != DBNull.Value)
                                {
                                    user.Email = (string)reader["Email"];
                                }
                                getAttended.Add(user);
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
            return getAttended;
        }

        public void CreateGameAttance(GameAttendance gameAttended)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("CreateGameAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@gid", gameAttended.GameID);
                        command.Parameters.AddWithValue("@uid", gameAttended.UserID);
                        command.Parameters.AddWithValue("@attend", gameAttended.Attended);
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

        public void CreatePracticeAttendance(PracticeAttended practiceAttended)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("CreatePracticeAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@uid", practiceAttended.UserID);
                        command.Parameters.AddWithValue("@pid", practiceAttended.PracticeID);
                        command.Parameters.AddWithValue("@attend", practiceAttended.Attended);
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

        public List<GameAttendance> getGameAttendance()
        {
            //make a list to store Games Attendance
            List<GameAttendance> getAttended = new List<GameAttendance>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_AllAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameAttendance attendance = new GameAttendance();
                                if (reader["gameID"] != DBNull.Value)
                                {
                                    attendance.GameID = (int)reader["gameID"];
                                }
                                else { }
                                if (reader["userID_fk"] != DBNull.Value)
                                {
                                    attendance.UserID = (int)reader["userID_fk"];
                                }
                                else { }
                                if (reader["attended"] != DBNull.Value)
                                {
                                    attendance.Attended = (bool)reader["attended"];
                                }
                                else { }
                                getAttended.Add(attendance);
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
            return getAttended;
        }
        public List<PracticeAttended> getPracticeAttendaned(int id)
        {
            //make a list to store Games Attendance
            List<PracticeAttended> getAttended = new List<PracticeAttended>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewPracticeAttendance", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@id", id);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PracticeAttended attendance = new PracticeAttended();
                                if (reader["userID_fk"] != DBNull.Value)
                                {
                                    attendance.UserID = (int)reader["userID_fk"];
                                }
                                else
                                {
                                    attendance.UserID = 0;
                                }
                                if (reader["practiceID"] != DBNull.Value)
                                {
                                    attendance.PracticeID = (int)reader["practiceID"];
                                }
                                else
                                {
                                    attendance.PracticeID = 0;
                                }
                                if (reader["attended"] != DBNull.Value)
                                {
                                    attendance.Attended = (bool)reader["attended"];
                                }
                                else
                                {
                                   
                                }
                                getAttended.Add(attendance);
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
            return getAttended;
        }
    }
}    


  
        
 
