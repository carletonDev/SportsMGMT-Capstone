

namespace SportsMGMTDataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;

    //connection to CRUD games
    public class GameDataAccess:IGameDataAccess
    {
        public string Connection = AppSettings.Default.ConnectionString;
        //Read Games
        public List<Game>GetGames()
        {
            //make a list to store Games
            List<Game> getGames = new List<Game>();
            //try to populate the list
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetGames", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        con.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Game game = new Game();
                                game.GameID = (int)reader["gameID"];
                                if (reader["start_time"] != DBNull.Value)
                                {
                                    game.StartTime = (DateTime)reader["start_time"];
                                }
                                if (reader["end_time"] != DBNull.Value)
                                {
                                    game.EndTime = (DateTime)reader["end_time"];
                                }
                                if (reader["hometeam_fk"] != DBNull.Value)
                                {
                                    game.HomeTeam = (int)reader["hometeam_fk"];
                                }
                                if (reader["awayteam_fk"] != DBNull.Value)
                                {
                                    game.AwayTeam = (int)reader["awayteam_sk"];
                                }
                                if (reader["hometeam_score"] != DBNull.Value)
                                {
                                    game.HomeTeamScore = (int)reader["hometeam_score"];
                                }
                                if (reader["awayteam_score"] != DBNull.Value)
                                {
                                    game.AwayTeamScore = (int)reader["awayteam_score"];
                                }
                                getGames.Add(game);


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
            return getGames;
        }
        //Create Games
        public void CreateGame(Game game)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("CreateGame", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        if (game.StartTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@start", game.StartTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@start", DateTime.Now);
                        }
                        if (game.EndTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@end", game.EndTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@end", DateTime.Now);
                        }
                        if(game.HomeTeam != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@ht", game.HomeTeam);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ht", 0);
                        }
                        if (game.AwayTeam != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@at", game.AwayTeam);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@at", 0);
                        }
                        if(game.HomeTeamScore != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@hts", game.HomeTeamScore);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@hts", 0);
                        }
                        if (game.HomeTeamScore != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@ats", game.AwayTeamScore);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ats", 0);
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
        //Update Game Store 
        public void UpdateGameScore(Game game)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdateGameScore", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@home", game.HomeTeamScore);
                        command.Parameters.AddWithValue("@away", game.AwayTeamScore);
                        command.Parameters.AddWithValue("@id", game.GameID);
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
        //Delete game
        public void DeleteGame(Game game)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteGame", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@id", game.GameID);
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
        //Update Game on all values
        public void UpdateGame(Game game)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Connection))
                {
                    using (SqlCommand command = new SqlCommand("sp_UpdateGame", con))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 10;
                        command.Parameters.AddWithValue("@id", game.GameID);
                        if (game.StartTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@start", game.StartTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@start", DateTime.Now);
                        }
                        if (game.StartTime != DateTime.MinValue)
                        {
                            command.Parameters.AddWithValue("@end", game.EndTime);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@end", DateTime.Now);
                        }
                        if (game.HomeTeam != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@ht", game.HomeTeam);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ht", DBNull.Value);
                        }
                        if (game.AwayTeam != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@at", game.AwayTeam);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@at", DBNull.Value);
                        }
                        if (game.HomeTeamScore != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@hts", game.HomeTeamScore);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@hts", DBNull.Value);
                        }
                        if (game.AwayTeamScore != int.MinValue)
                        {
                            command.Parameters.AddWithValue("@ats", game.AwayTeamScore);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@ats", DBNull.Value);
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
