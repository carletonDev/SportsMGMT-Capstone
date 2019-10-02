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

namespace SportsMGMTApp.Models
{
    public class GameAttendanceModel
    {
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
            GameBLL game = new GameBLL(); // create a new Game BLL object
            List<Game> homeGames = game.GetGames().FindAll(m => m.HomeTeam == id); //Find All coach admins Home Games
            List<Game> awayGames = game.GetGames().FindAll(m => m.AwayTeam == id); //Find All coach admins Away Games
            List<Game> allTeamGames = new List<Game>(); //create a list that combines all games for Team
            allTeamGames.AddRange(homeGames); //add home games to list
            allTeamGames.AddRange(awayGames); //add away games to list
            Game games = new Game();
            return allTeamGames;    
        }

        public  static string FormatGameIDs(int id) //format the game id so it appears as coach team vs team
        {
            GameBLL games = new GameBLL(); //create a new game bll object
            Game game = games.GetGames().Find(m => m.GameID == id); //store the game details in an object
            TeamBLL teamBLL = new TeamBLL(); //create a team object
            List<Team> getTeams = teamBLL.GetTeams(); //get list of all team
            Team homeTeam = getTeams.Find(m => m.TeamID == game.HomeTeam); //make home and away team obj
            Team awayTeam = getTeams.Find(m => m.TeamID == game.AwayTeam);
            //Format the Game
            string matchUP = homeTeam.TeamName + " " + "vs" + " " + awayTeam.TeamName; //format the string for game id
            return matchUP; //return string
        }
        
        public static string GetUserName(int id)
        {
            UsersBLL usersBLL = new UsersBLL();
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
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            List<GameAttendance> games = attendanceBLL.getGameAttendaned();
            return games;
        }

        public IEnumerable<SelectListItem> GetUsers(int teamid)
        {
            UsersBLL usersBLL = new UsersBLL();

            return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == teamid), "UserID", "FullName");
        
        }
    }
}