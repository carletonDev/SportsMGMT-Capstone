namespace SportsMGMTApp.Controllers
{
    using SportsMGMTApp.Models;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Collections;
    using SportsMGMTApp.Filters;
    public class StatsController : Controller
    {
        // GET: Stats
        public ActionResult Charts()
        {
            //populate stats model for visual display in charts view using charts.js
            //each method is named for the chart it displays
            StatsModel stats  = AbsentChart();
            var user = Session["Users"] as Users;
            GameStatsChart(stats,user.TeamID);
            PieChartPoints(stats);
            PieChartRebounds(stats);
            Session["stats"] = stats;
            
                
         return View();
        }
        
        // a list For all users to see stats
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach,Player")]
        public ActionResult StatTable()
        {
            PlayerStatsBLL playerStats = new PlayerStatsBLL();
            var user = Session["Users"] as Users;

            List<PlayerStats> statTable = playerStats.GetStats().FindAll(m => m.TeamID == user.TeamID);

            return View(statTable);
        }
        //update player stats don't need team id same team
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult UpdatePlayerStats(int id)
        {
            PlayerStatsModel player = new PlayerStatsModel();
            PlayerStatsBLL updatePlayer = new PlayerStatsBLL();
            PlayerStats playerUpdated = updatePlayer.GetStats().Find(m => m.StatID == id);
            player.StatID = playerUpdated.StatID;
            player.Misses = playerUpdated.Misses;
            player.GameID = playerUpdated.GameID;
            player.Assists = playerUpdated.Assists;
            player.Points = playerUpdated.Points;
            player.Rebounds = playerUpdated.Rebounds;
            player.UserID = playerUpdated.UserID;



            return View(player);
        }
        //post update player stats
        [HttpPost]
        [MustBeInRole(Roles = "Coach,Admin")]
        public ActionResult UpdatePlayerStats(PlayerStatsModel player)
        {
            if (ModelState.IsValid)
            {
                PlayerStatsBLL updatePlayer = new PlayerStatsBLL();

                PlayerStats checkUpdate = updatePlayer.GetStats().Find(m => m.StatID == player.StatID);

                var user = Session["Users"] as Users;
                PlayerStats playerUpdate = new PlayerStats();
                playerUpdate.Misses = player.Misses;
                playerUpdate.Points = player.Points;
                playerUpdate.Rebounds = player.Rebounds;
                playerUpdate.UserID = player.UserID;
                playerUpdate.Assists = player.Assists;
                playerUpdate.TeamID = user.TeamID;
                playerUpdate.GameID = player.GameID;
                playerUpdate.StatID = player.StatID;

                updatePlayer.UpdateStats(playerUpdate);

                PlayerStats updatedPlayer = updatePlayer.GetStats().Find(m => m.StatID == player.StatID);

                if(updatedPlayer != checkUpdate)
                {
                    ViewBag.Message = "Update Successful";
                }
                else if(updatedPlayer ==checkUpdate)
                {
                    ViewBag.Message = "Update Failed";
                    return View(player);
                }
                else
                {


                }
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
            }
            return View(player);
        }
        //Get Create Stat View
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult CreatePlayerStats(int id)
        {
            PlayerStatsModel playerStats = new PlayerStatsModel();
            playerStats.GameID = id;
            return View(playerStats);
        }
        //Post Create Stat View
        [HttpPost]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult CreatePlayerStats(PlayerStatsModel player)
        {
            if (ModelState.IsValid)
            {
                var user = Session["Users"] as Users;
                PlayerStatsBLL createPlayer = new PlayerStatsBLL();
                PlayerStats insertPlayer = new PlayerStats();
                insertPlayer.Misses = player.Misses;
                insertPlayer.Points = player.Points;
                insertPlayer.Rebounds = player.Rebounds;
                insertPlayer.UserID = player.UserID;
                insertPlayer.Assists = player.Assists;
                insertPlayer.TeamID = user.TeamID;
                insertPlayer.GameID = player.GameID;

                bool checkDuplicate = createPlayer.GetStats().Exists(m => m.GameID == insertPlayer.GameID && m.UserID == insertPlayer.UserID);

                if (checkDuplicate)
                {
                    ViewBag.Message = "Stat already created for Player Please Update!";
                    return View(player);
                }
                else
                {
                    createPlayer.InsertStats(insertPlayer);
                }

                bool checkPlayer = createPlayer.GetStats().Exists(m => m.GameID == insertPlayer.GameID && m.UserID == insertPlayer.UserID);


                if (checkPlayer)
                {
                    ViewBag.Message = "Player Stat Created!";
                }
                else if (checkPlayer == false)
                {
                    ViewBag.Message = "Player Stat Was Not Created!";
                    return View(player);
                }
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
            }

            return View(player);
        }
        //returns a stat model for absent charts for practice with averages for benchmarks for attendance
        //each chart uses the color randomizer for cool color changing properties
        public StatsModel AbsentChart()
        {
            //List of Jpeg Charts to Display
            StatsModel stats = new StatsModel();
            //Attendance pie chart
            UsersBLL usersBLL = new UsersBLL();
            var user = Session["Users"] as Users;
            List<Users> users = usersBLL.GetUsers().FindAll(m => m.TeamID == user.TeamID && m.RoleID !=2);
            List<CountAttendance> count = new List<CountAttendance>();
            //store each players practice attendance ratios in a list for each player on the team
            foreach (Users player in users)
            {
                CountAttendance checkAttendance = MeaningfulCalculation.GetPracticeAttendanceUser(player.TeamID,player.UserID);
                count.Add(checkAttendance);
            }
            //get x and y values in array
            ArrayList xValue = new ArrayList();
            int[] yValue = new int[count.Count];
            int[] absent = new int[count.Count];
            int[] average = new int[count.Count];
            int[] absentAverage = new int[count.Count];
            int x = 0;
            foreach (CountAttendance attendance in count)
            {
                xValue.Add(attendance.FullName.ToString());
                yValue[x]=attendance.NumPresent;
                absent[x] = attendance.NumAbsent;
                x++;
            }

            // place the names number present absent and the averages for line graphs
            stats.xValue = xValue;
            stats.Present = yValue;
            for (x = 0; x < stats.xValue.Count; x++)
            {
                average[x]= yValue.Sum() / yValue.Count();
                absentAverage[x]= absent.Sum() / absent.Count();
            }
            stats.Average = average;
            stats.Absent = absent;
            stats.AverageAbsent = absentAverage;

            
            //store in Session
            return stats;
        }
        //create a bar and line graph that shows the averages as benchmarks to compare the players statistics
        public void GameStatsChart(StatsModel stats,int teamID)
        {
            //potentially refractor this into a method later depends
            PlayerStatsBLL playerStatsBLL = new PlayerStatsBLL();
            UsersBLL users = new UsersBLL();
            //make list to store all player stats for all games
            List<PlayerStats> AllStatsForTeam = new List<PlayerStats>();
            //find all players on team and filter out coaches
            List<Users> myTeam = users.GetUsers().FindAll(m => m.TeamID == teamID && m.RoleID !=2);
            ArrayList playerNames = new ArrayList();
            //Get a List of onlyPlayers names
            foreach(Users user in myTeam)
            {
                playerNames.Add(user.FullName);
            }
            //store in array
            stats.onlyPlayers = playerNames;
            //initalize arrays for the players Total stats
            int[] points = new int[myTeam.Count];
            int[] assists = new int[myTeam.Count];
            int[] rebounds = new int[myTeam.Count];
            int[] misses = new int[myTeam.Count];
            //Get player stats
            int x = 0;
            foreach(Users user in myTeam)
            {
                //Get the stats for the player for each game
                List<PlayerStats> getPlayerStats = playerStatsBLL.GetStats().FindAll(m => m.UserID == user.UserID);
                //sum all points and store in array
                int pointsTotal = 0;
                foreach(PlayerStats player in getPlayerStats)
                {
                    pointsTotal += player.Points;
                }
                points[x] = pointsTotal;
                //sum all assists and store in array
                int assistsTotal = 0;
                foreach (PlayerStats player in getPlayerStats)
                {
                    assistsTotal += player.Assists;
                }
                assists[x] = assistsTotal;
                //sum all rebounds and store in array
                int reboundsTotal = 0;
                foreach(PlayerStats player in getPlayerStats)
                {
                    reboundsTotal += player.Rebounds;
                }
                rebounds[x] = reboundsTotal;
                //sum all misses and store in array
                int missesTotal = 0;
                foreach(PlayerStats player in getPlayerStats)
                {
                    missesTotal += player.Misses;
                }
                misses[x] = missesTotal;
                //increment the arrays to store the next players total stats
                x++;
            }
            //set them into the passed in object
            stats.Points = points;
            stats.Assists = assists;
            stats.Rebounds = rebounds;
            stats.Misses = misses;

            //Get the Averages
            int[] pointAverage = new int[myTeam.Count];
            int[] missAverage = new int[myTeam.Count];
            int[] assistsAverage = new int[myTeam.Count];
            int[] reboundsAverage = new int[myTeam.Count];

            for(int y = 0; y < myTeam.Count; y++)
            {
                pointAverage[y] = points.Sum() / myTeam.Count;
                missAverage[y] = misses.Sum() / myTeam.Count;
                assistsAverage[y] = assists.Sum() / myTeam.Count;
                reboundsAverage[y] = rebounds.Sum() / myTeam.Count;
            }
            //set them in object
            stats.GameAssistsAverage = assistsAverage;
            stats.GameMissAverage = missAverage;
            stats.GamePointAverage = pointAverage;
            stats.GameReboundAverage = reboundsAverage;
        }
        //create a pie chart of the point distribution to see who are the go to scorers
        public void PieChartPoints(StatsModel stats)
        {
            //Get the sum of all points as a total
            //for each point in the array
            decimal sumAll = Convert.ToDecimal(stats.Points.Sum());
            decimal[] percentage = new decimal[stats.Points.Count()];
            for(int x =0; x < stats.Points.Count(); x++)
            {
                decimal number = Convert.ToDecimal(stats.Points[x]);
                decimal addToArray = number/sumAll;
                
                //store the percentage for each player in the array
                percentage[x]=Math.Round(addToArray,2);
            }
            stats.PiePoints = percentage;
            //get the percentage of the total points that player has earned

            //store in array random colors for the pie chart for chart.js
            stats.ColorRandom = MeaningfulCalculation.RandomColor(stats.Points.Count());


        }
        public void PieChartRebounds(StatsModel stats)
        {
            //Get the sum of all rebounds as a total
            //for each rebounds in the array
            decimal sumAll = Convert.ToDecimal(stats.Rebounds.Sum());
            decimal[] percentage = new decimal[stats.Rebounds.Count()];
            for (int x = 0; x < stats.Points.Count(); x++)
            {
                decimal number = Convert.ToDecimal(stats.Rebounds[x]);
                decimal addToArray = number / sumAll;

                //store the percentage for each player in the array
                percentage[x] = Math.Round(addToArray, 2);
            }
            stats.PieRebounds = percentage;
            //get the percentage of the total rebounds that player has earned

            //store in array random colors for the pie chart for chart.js
            stats.ColorRandom = MeaningfulCalculation.RandomColor(stats.Points.Count());
        }

    }
}