
namespace SportsMGMTDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    //connection to CRUD and store  Player Stats
    public class PlayerStatsDA:IPlayerStatsDA
    {
        //declare connection string and reader values
        public string Connection = AppSettings.Default.ConnectionString;
        string statID = "statID";string userID = "userID_fk";string gameID = "gameID_fk"; string points = "points";
        string assists = "assists";string rebounds = "rebounds"; string misses = "misses"; string teamID = "teamID_fk";
        //Get A List of Players Stats 
        public List<PlayerStats> GetPlayerStats()
        {
            //make a list to store Players Stats
            List<PlayerStats> getStats = new List<PlayerStats>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_ViewStats", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlayerStats player = new PlayerStats();
                                player.StatID = (int)reader[statID];
                                if (reader[userID] != DBNull.Value)
                                {
                                    player.UserID = (int)reader[userID];
                                }
                                else { }
                                if (reader[gameID] != DBNull.Value)
                                {
                                    player.GameID = (int)reader[gameID];
                                }
                                else { }
                                if (reader[points] != DBNull.Value)
                                {
                                    player.Points = (int)reader[points];
                                }
                                else { }
                                if (reader[assists] != DBNull.Value)
                                {
                                    player.Assists = (int)reader[assists];
                                }
                                else { }
                                if (reader[rebounds] != DBNull.Value)
                                {
                                    player.Rebounds = (int)reader[rebounds];
                                }
                                else { }
                                if (reader[misses] != DBNull.Value)
                                {
                                    player.Misses = (int)reader[misses];
                                }
                                else
                                {

                                }
                                if(reader[teamID] != DBNull.Value)
                                {
                                    player.TeamID = (int)reader[teamID];
                                }
                                else { }
                                getStats.Add(player);
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
            return getStats;
        }
        //Insert Player Stats
        public void InsertPlayerStats(PlayerStats player)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_CreateStat", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@uid", player.UserID);
                        command.Parameters.AddWithValue("@gid", player.GameID);
                        command.Parameters.AddWithValue("@points", player.Points);
                        command.Parameters.AddWithValue("@assists", player.Assists);
                        command.Parameters.AddWithValue("@rebounds", player.Rebounds);
                        command.Parameters.AddWithValue("@misses", player.Misses);
                        command.Parameters.AddWithValue("@team", player.TeamID);
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
        //Update Player Stats
        public void UpdatePlayerStats(PlayerStats player)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdatePlayerStats", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@sid", player.StatID);
                        command.Parameters.AddWithValue("@uid", player.UserID);
                        command.Parameters.AddWithValue("@gid", player.GameID);
                        command.Parameters.AddWithValue("@points", player.Points);
                        command.Parameters.AddWithValue("@assists", player.Assists);
                        command.Parameters.AddWithValue("@rebounds", player.Rebounds);
                        command.Parameters.AddWithValue("@misses", player.Misses);
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
        //Delete Player Stats
        public void DeletePlayerStats(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteStats", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@sid", id);
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

