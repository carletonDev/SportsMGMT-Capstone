
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
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    //Creates the connection for the app attendance tables and bar chart
    public class AttendanceDataAccess:IAttendanceDataAccess
    {
        string Connection =AppSettings.Default.ConnectionString;
        //Get A List of User who attended or didnt attend practice depending on input provided

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
                                else { attendance.GameID = GameAttendance.Null.GameID; }
                                if (reader["userID_fk"] != DBNull.Value)
                                {
                                    attendance.UserID = (int)reader["userID_fk"];
                                }
                                else { attendance.UserID = GameAttendance.Null.UserID; }
                                if (reader["attended"] != DBNull.Value)
                                {
                                    attendance.Attended = (bool)reader["attended"];
                                }
                                else { attendance.Attended = GameAttendance.Null.Attended; }
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
                                    attendance.UserID = PracticeAttended.Null.UserID;
                                }
                                if (reader["practiceID"] != DBNull.Value)
                                {
                                    attendance.PracticeID = (int)reader["practiceID"];
                                }
                                else
                                {
                                    attendance.PracticeID = PracticeAttended.Null.PracticeID;
                                }
                                if (reader["attended"] != DBNull.Value)
                                {
                                    attendance.Attended = (bool)reader["attended"];
                                }
                                else
                                {
                                    attendance.Attended = PracticeAttended.Null.Attended;
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


  
        
 
