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
        public List<Team> GetTeamName()
        {
            TeamBLL Teams = new TeamBLL();
            List<Team> getTeam = Teams.GetTeams();

            return getTeam; 
        }
        //Gets Name of Team based on Foreign Keys can be called without constructing it
        public static string GetTeamNameString(int id)
        {
            GameModel gameModel = new GameModel();
            Team team = gameModel.GetTeamName().Find(m => m.TeamID == id);
            return team.TeamName;
        }
        public List<Game> GetGames()
        {
            GameBLL Game = new GameBLL();
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