

namespace SportsMGMTDataAccess
{
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    //Establishes connection for CRUD Practice Objects
    public class PracticeDataAccess : IPracticeDataAccess
    {
        public string Connection = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        //Get a List of Practices
        public List<Practice>GetPractice()
        {
            //make a list to store Practice
            List<Practice> getPractice = new List<Practice>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetPractice", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Practice practice = new Practice();
                                practice.PracticeID = (int)reader["practiceID"];
                                if (reader["practice_type"] == DBNull.Value)
                                {
                                    practice.PracticeType = "No Practice Recorded";
                                }
                                else { practice.PracticeType = (string)reader["practice_type"]; }
                                if (reader["start_time"] == DBNull.Value)
                                {

                                }
                                else { practice.StartTime = (DateTime)reader["start_time"]; }
                                if (reader["end_time"] == DBNull.Value)
                                {

                                }
                                else { practice.EndTime = (DateTime)reader["end_time"]; }
                                if (reader["teamID_fk"] == DBNull.Value)
                                {

                                }
                                else {practice.TeamID = (int)reader["teamID_fk"]; }
                                getPractice.Add(practice);
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
            return getPractice;
        }
        //Update Start Time and End Times for Practice
        public void UpdatePractice(Practice practice)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdatePractice", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@start", practice.StartTime);
                        command.Parameters.AddWithValue("@end", practice.EndTime);
                        command.Parameters.AddWithValue("@id", practice.PracticeID);
                        command.Parameters.AddWithValue("@type", practice.PracticeType);
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
        //Delete Practice By Id
        //Demonstrate cascading Delete
        public void DeletePracticeById(Practice practice)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("deletePracticeById", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@id", practice.PracticeID);
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
        //Create Practice
        public void CreatePractice(Practice practice)
        {
                try
                {
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_addpractice", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                            command.Parameters.AddWithValue("@type", practice.PracticeType);
                        if (practice.StartTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@start", practice.StartTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@start", DateTime.Now);
                        }
                        if (practice.StartTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@end", practice.EndTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@end", DateTime.Now);
                        }
                        if (practice.TeamID != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@tid", practice.TeamID);
                        }
                        else
                        {

                        }
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


