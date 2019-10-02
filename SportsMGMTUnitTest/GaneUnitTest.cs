using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTCommon;
using SportsMGMTBLL;
using System.Collections.Generic;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class GameUnitTest
    {
        [TestMethod]
        public void GetGames()
        {
            //Arrange
            GameBLL gameBLL = new GameBLL();
            //Act
            List<Game> getGames = gameBLL.GetGames();
            //Assert
            Assert.IsTrue(getGames.Exists(m => m.GameID == 3));
        }
        [TestMethod]
        public void CreateGame()
        {
            //Arrange
            GameBLL gameBLL = new GameBLL();
            Game game = new Game();
            TeamBLL teamBLL = new TeamBLL();
            Team team = new Team();
            List<Team> getTeams = teamBLL.GetTeams();

            //Act
            //find team by name and store in an object
            team = getTeams.Find(m => m.TeamName == "Richmond Lovers");
            game.AwayTeam = team.TeamID;
            team = getTeams.Find(m => m.TeamName == "Macon SmallTowners");
            game.HomeTeam = team.TeamID;
            game.HomeTeamScore = 101;
            game.AwayTeamScore = 105;
            game.StartTime = DateTime.Now;
            game.EndTime = Convert.ToDateTime("6/28/2019 5:00PM");
            //insert game object into database
            gameBLL.CreateGame(game);
            //Get a new list of teams to check if data entered is correct
            List<Game> getGames = gameBLL.GetGames();

            //Assert
            Assert.IsTrue(getGames.Exists(m => m.EndTime == Convert.ToDateTime("6/28/2019 5:00PM")));
        }

        [TestMethod]
        public void UpdateGameScore()
        {
            //Arrange
            //Get a list of Games
            GameBLL gameBLL = new GameBLL();
            List<Game> getGames = gameBLL.GetGames();
            //Populate a game object with the updated Fame
            Game game = getGames.Find(m => m.EndTime == Convert.ToDateTime("6/28/2019 5:00PM"));
            game.HomeTeamScore = 107;
            gameBLL.UpdateGameScore(game);
            List<Game> checkGames = gameBLL.GetGames();
            //Assert the Game Updated correctly
            Assert.IsTrue(checkGames.Exists(m => m.HomeTeamScore == 107));
        }
        [TestMethod]
        public void UpdateGame()
        {
            //Arrange
            //Get a list of Games
            GameBLL gameBLL = new GameBLL();
            List<Game> getGames = gameBLL.GetGames();
            //Populate a game object with the updated Fame
            Game game = getGames.Find(m => m.EndTime == Convert.ToDateTime("6/28/2019 5:00PM"));
            game.HomeTeamScore = 109;
            gameBLL.UpdateGame(game);
            List<Game> checkGames = gameBLL.GetGames();
            //Assert the Game Updated correctly
            Assert.IsTrue(checkGames.Exists(m => m.HomeTeamScore == 109));
        }
        [TestMethod]
        public void DeleteGame()
        {
            //Arrange
            //Get a list of Games
            GameBLL gameBLL = new GameBLL();
            List<Game> getGames = gameBLL.GetGames();
            //Populate a game object with the updated Fame
            Game game = getGames.Find(m => m.GameID == 6);
            gameBLL.DeleteGame(game);
            List<Game> checkGames = gameBLL.GetGames();
            //Assert the Game Deleted correctly
            Assert.IsFalse(checkGames.Exists(m => m.GameID==6));
        }
    }
}
