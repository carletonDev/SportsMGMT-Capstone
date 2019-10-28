

namespace SportsMGMTDataAccess
{ //CRUD Teams Data Access
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    public class TeamDataAccess:ITeamDataAccess
    {
        public string Connection = AppSettings.Default.ConnectionString;
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
                        command.CommandTimeout = 30;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Team team = new Team();
                                if (reader["teamID"] != DBNull.Value)
                                {
                                    team.TeamID = (int)reader["teamID"];
                                }
                                else { team.TeamID = Team.Null.TeamID; }
                                team.SalaryCap = reader["salary_cap"]!=DBNull.Value?(decimal)reader["salary_cap"]:Team.Null.SalaryCap;
                                team.TeamName = reader["team_name"]!=DBNull.Value?(string)reader["team_name"]:Team.Null.TeamName;
                                team.TeamType = reader["team_type"]!=DBNull.Value?(string)reader["team_type"]:Team.Null.TeamType;
                                if (reader["wins"] != DBNull.Value)
                                {
                                    team.Wins = (int)reader["wins"];
                                }
                                else
                                {
                                    team.Wins = Team.Null.Wins;
                                }
                                if (reader["losses"] != DBNull.Value)
                                {
                                    team.Losses = (int)reader["losses"];
                                }
                                else
                                {
                                    team.Losses = Team.Null.Losses;
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

    }
}
