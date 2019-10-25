using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsMGMTCommon;
using SportsMGMTBLL;
using SportsMGMTApp.Filters;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using SportsMGMTApp.Controllers;
using Interfaces.IBusinessLogic;

namespace SportsMGMTApp.Models
{
    public class GameAttendanceModel
    {
       public static IGame games;
        public static ITeam teamBLL;
        public static IUser usersBLL;
        public static IAttendanceBLL attendanceBLL;
        public GameAttendanceModel(IGame game, ITeam team,IUser user, IAttendanceBLL attendance)
        {
            games = game;
            teamBLL = team;
            usersBLL = user;
            attendanceBLL = attendance;
        }
        [Required]
        public int GameID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public bool Attended { get; set; }
        public List<Users> Players { get; set; }
        public List<Game> Games { get; set; }

        public static List<Game> GetGames(int id)
        {
            List<Game> homeGames = games.GetGames().FindAll(m => m.HomeTeam == id); //Find All coach admins Home Games
            List<Game> awayGames = games.GetGames().FindAll(m => m.AwayTeam == id); //Find All coach admins Away Games
            List<Game> allTeamGames = new List<Game>(); //create a list that combines all games for Team
            allTeamGames.AddRange(homeGames); //add home games to list
            allTeamGames.AddRange(awayGames); //add away games to list
  
            return allTeamGames;    
        }

        public  static string FormatGameIDs(int id) //format the game id so it appears as coach team vs team
        {
            Game game = games.GetGames().Find(m => m.GameID == id); //store the game details in an object
            List<Team> getTeams = teamBLL.GetTeams(); //get list of all team
            Team homeTeam = getTeams.Find(m => m.TeamID == game.HomeTeam); //make home and away team obj
            Team awayTeam = getTeams.Find(m => m.TeamID == game.AwayTeam);
            //Format the Game
            string matchUP = homeTeam.TeamName + " " + "vs" + " " + awayTeam.TeamName; //format the string for game id
            return matchUP; //return string
        }
        
        public static string GetUserName(int id)
        {

            Users user = new Users();
            string name = "";
            if (id != 0)
            {
                 user = usersBLL.GetUsers().Find(m => m.UserID == id);
                 name = user.FullName;
            }
            else
            {
                name = "NULL";
            }
            return name;
        }
        //format attendance
        public static string FormatAttend(bool attend)
        {
            string check = "";
            if (attend)
            {
                check = "Present";
            }
            else if (attend == false)
            {
                check = "Absent";
            }
            else
            {
                check = "No Attendance Entered";
            }
            return check;
        }
        public List<GameAttendance> GetAttendance() // gets a list of users who have attended the class
        {

            List<GameAttendance> games = attendanceBLL.getGameAttendaned();
            return games;
        }

        public IEnumerable<SelectListItem> GetUsers(int teamid)
        {

            return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == teamid), "UserID", "FullName");
        
        }
    }
}