using Interfaces.IBusinessLogic;
using SportsMGMTBLL;
using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsMGMTApp.Models
{
    public class GameModel
    {
       static IGame Game;
       static ITeam teams;
        public GameModel(IGame games,ITeam team)
        {
            Game = games;
            teams = team;
        }
        public List<Team> GetTeamName()
        {

            List<Team> getTeam = teams.GetTeams();

            return getTeam; 
        }
        //Gets Name of Team based on Foreign Keys can be called without constructing it
        public static string GetTeamNameString(int id)
        {
            GameModel gameModel = new GameModel(Game,teams);
            Team team = gameModel.GetTeamName().Find(m => m.TeamID == id);
            return team.TeamName;
        }
        public List<Game> GetGames()
        {
            List<Game> getGame = Game.GetGames();

            return getGame;
        }
        public IEnumerable<SelectListItem> GetGame { get; set; }
        public IEnumerable<SelectListItem> GetTeam { get{
                 return new SelectList(GetTeamName(), "TeamID", "TeamName");
            } }
        [Required]
        
        public int GameId { get; set; }
        [Required]
        [DataType(DataType.DateTime,ErrorMessage ="Enter Appropriate Start-time")]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Enter Appropriate Start-time")]
        public DateTime EndTime { get; set; }
        [Required]
        public int HomeTeam { get; set; }
        [Required]
        public int AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        public Game game { get; set; }
    }
}