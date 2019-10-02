

namespace SportsMGMTDataAccess
{ //CRUD Teams Data Access
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    public class TeamDataAccess
    {
        //string Connection = "Data Source=DESKTOP-H52G7QL\\SQLEXPRESS;Itnitial Catalog=SportsMGMT-capstone;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public string Connection = ConfigurationManager.ConnectionStrings["Sports"].ConnectionString;
        //Uses the stored procedure Get teams to get the current Teams stored in the database
        public List<Team> GetTeams()
        {
            //make a list to store roles
            List<Team> getTeams = new List<Team>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetTeams", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Team team = new Team();
                                if (reader["TeamID"] != DBNull.Value)
                                {
                                    team.TeamID = (int)reader["teamID"];
                                }
                                else { team.TeamID = 0; }
                                team.SalaryCap = (decimal)reader["salary_cap"];
                                team.TeamName = (string)reader["team_name"];
                                team.TeamType = (string)reader["team_type"];
                                if (reader["wins"] != DBNull.Value)
                                {
                                    team.Wins = (int)reader["wins"];
                                }
                                else
                                {

                                }
                                if (reader["losses"] != DBNull.Value)
                                {
                                    team.Losses = (int)reader["losses"];
                                }
                                else
                                {

                                }
                                getTeams.Add(team);
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
            return getTeams;
        }

        //Create Teams using Team common objects
        public void CreateTeam(Team team)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_addteam", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@salary", team.SalaryCap);
                        command.Parameters.AddWithValue("@team", team.TeamName);
                        command.Parameters.AddWithValue("@type", team.TeamType);
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
        //Update Teams using Team common objects passes all values
        public void UpdateTeam(Team team)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdateTeam", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@cap", team.SalaryCap);
                        command.Parameters.AddWithValue("@name", team.TeamName);
                        command.Parameters.AddWithValue("@type", team.TeamType);
                        command.Parameters.AddWithValue("@wins", team.Wins);
                        command.Parameters.AddWithValue("@losses", team.Losses);
                        command.Parameters.AddWithValue("@id", team.TeamID);
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
        //Delete Teams using Team common objects passes in team name only
        public void DeleteTeam(Team team)

        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("DeleteTeam", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@name", team.TeamName);
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
        //Check Teams Salary Cap Remaining
        public decimal GetTeamSalaryCapRemaining(Team team)
        {
           
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetSalaryRemaining", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@name",team.TeamName);
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            //for single reader read
                            if (reader.Read())
                            {
                                team.SalaryCap = (decimal)reader["CapSpace"];
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
            //set it to temporary salary cap to store the value
            return team.SalaryCap;
        }
    }
}
