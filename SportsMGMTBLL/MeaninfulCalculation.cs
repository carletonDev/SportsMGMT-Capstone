namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public  class MeaningfulCalculation
    {
        //Meaningful Calulation to count the number of Attendance Vs Absence per game 
       public static IAttendanceBLL attendance;
        public static  IUser usersBLL;
        public static ITeam team;
        public static IContracts contractsBLL;
        public static IPractice practiceBLL;
        public MeaningfulCalculation(IAttendanceBLL attend, IUser user,ITeam teamBLL,IContracts contracts,IPractice practice)
        {
            attendance = attend;
            usersBLL = user;
            team = teamBLL;
            contractsBLL = contracts;
            practiceBLL = practice;
        }
        public static CountAttendance GetPracticeAttendanceUser(int teamid, int userid)
        {
            //create count attendance variable
            CountAttendance count = new CountAttendance();
            //get the users full name
            count.FullName = usersBLL.GetUsers().Find(m => m.UserID == userid).FullName;
            //get the total attendance for the team
            List<PracticeAttended> getAttendance = attendance.getPracticeAttendaned(teamid);
            //store in a list the total times the user has been present
            List<PracticeAttended> userPresent = getAttendance.FindAll(m => m.UserID == userid).FindAll(m => m.Attended == true);
            //store in a list the total times the user has been absent
            List<PracticeAttended> userAbsent = getAttendance.FindAll(m => m.UserID == userid).FindAll(m => m.Attended == false);
            //make another list to get the total number of times attendance has been tracked for the user
            List<PracticeAttended> total = new List<PracticeAttended>();
            //add both present and absent list to total
            total.AddRange(userPresent);
            total.AddRange(userAbsent);
            //count the number of absences and store in count
            count.NumAbsent = userAbsent.Count;
            //count the number of present
            count.NumPresent = userPresent.Count;
            //count the total number of times attendance has been tracked
            count.total = total.Count;
            //get a ratio of percentage absent
            if (count.total == 0)
            {
                count.AbsentRatio = 0;
                //get a percentage ratio of present
                count.PresentRatio = 0;
                //return attendance count for statistics
            }
            else
            {
                count.AbsentRatio = count.NumAbsent / count.total;
                //get a percentage ratio of present
                count.PresentRatio = count.NumAbsent / count.total;
                //return attendance count for statistics
            }

            return count;

        }
        //does the dashboard calculations for display
        public static DashBoard ReturnDashBoard(Users users)
        {
            //Users
            //var users = Session["Users"] as Users;
            //Instantiate the dashboard class for session variables

            DashBoard dashboard = new DashBoard();
            Team getTeam = new Team();
            List<Users> getUsers = new List<Users>();
            if (users.TeamID != 0)
            {
                getTeam = team.GetTeams().Find(m => m.TeamID == users.TeamID);
            }
            else { }
            //Get Average Contract
            if (users.TeamID != 0)
            {
                getUsers = usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            }
            else { }
            List<decimal> salaries = new List<decimal>();
            //Find Days remaining till Contract Expiration

            //find when the contract expires
            if (users.ContractDuration != 0)
            {
                dashboard.ContractExpires = users.ContractStart.AddDays(users.ContractDuration * 365);
            }
            TimeSpan time = (dashboard.ContractExpires - DateTime.Now);
            string daysRemaining = time.Days.ToString() + " Days, " + time.Hours.ToString() + " Hours, " + time.Minutes.ToString() + " Minutes";
            dashboard.DaysRemaining = daysRemaining;
            // Add users contracts to salary to sum and average

            foreach (Users user in getUsers)
            {
                Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == user.ContractID);
                salaries.Add(contracts.Salary);
            }
            //Get average
            decimal averageSalary =0.0M;
            if (salaries.Count >= 1)
            {
                averageSalary = salaries.Sum() / salaries.Count;
            }
            //store in dashboard
            dashboard.AverageSalary = averageSalary;
            //User Messages and Alerts
            //message for users with no teams

            List<Users> getUser = usersBLL.GetUsers().FindAll(m => m.TeamID == 0);
            getUser.AddRange(usersBLL.GetUsers().FindAll(m => m.TeamID == 1034));
            dashboard.NoTeam = getUser.Count;

            dashboard.FreeAgents = getUser;

            //Get Users Roster for his Team
            dashboard.MyRoster = usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            //Get Team Standings
            dashboard.Standings = team.GetTeams().FindAll(m => m.TeamType == "basketball");
            //sort by wins
            dashboard.Standings.Sort((x,y)=>y.Wins.CompareTo(x.Wins));
            //message for users with no roles for admin to notifiy
            
            List<Users> UsersRole = usersBLL.GetUsers().FindAll(m => m.RoleID == 0);
            dashboard.NoRoles = UsersRole.Count;

            //Salary Cap Remaining
            List<Users> FindCap = usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            List<decimal> CapSpace = new List<decimal>();
            //for each user in find cap
            foreach (Users users1 in FindCap)
            {
                //get contract object and add salary times contract year to the capspace list
                Contracts salary = contractsBLL.GetContracts().Find(m => m.ContractID == users1.ContractID);
                CapSpace.Add(Convert.ToDecimal(salary.Salary * users1.ContractDuration));
            }
            //Sum all the contracts
            dashboard.CapSpace = CapSpace.Sum();
            dashboard.TeamSalary = team.GetTeams().Find(m => m.TeamID == users.TeamID).SalaryCap;
            var result = dashboard.CapSpace / dashboard.TeamSalary;
            dashboard.PercentageCap = 1 - result;
            //Display Win Loss ratio
            dashboard.TeamWins = team.GetTeams().Find(m => m.TeamID == users.TeamID).Wins;
            dashboard.TeamLosses = team.GetTeams().Find(m => m.TeamID == users.TeamID).Losses;
            if (users.TeamID != 1034)
            {
                dashboard.MyTeam = team.GetTeams().Find(m => m.TeamID == users.TeamID).TeamName;
            }
            else
            {
                dashboard.MyTeam = "No Team";
            }
            //find all upcoming games and practices for the next two weeks for notifications for the players

            GameBLL games = new GameBLL();
            List<Game> homeGames = games.GetGames().FindAll(m => m.HomeTeam == users.TeamID);
            List<Game> awayGame = games.GetGames().FindAll(m => m.AwayTeam == users.TeamID);

            List<Game> allGames = new List<Game>();
            //store all users games into the game table
            allGames.AddRange(homeGames);
            allGames.AddRange(awayGame);

            //Get all practices

            List<Practice> teamPractices = practiceBLL.GetPractice().FindAll(m => m.TeamID == users.TeamID);

            //check time span find the game one week away
            List<Game> weeklyGame = new List<Game>();
            foreach (Game game in allGames)
            {
                TimeSpan gameTime = game.StartTime-DateTime.Now;
                if (gameTime.Days >= 1)
                {
                    weeklyGame.Add(game);
                }

            }
            //same thing for all practices
            List<Practice> WeeklyPractice = new List<Practice>();
            foreach(Practice practice in teamPractices)
            {
                TimeSpan practiceTime = practice.StartTime-DateTime.Now;
                if(practiceTime.Days >= 1)
                {
                    WeeklyPractice.Add(practice);
                }
            }

            //sort weekly game and practice
            weeklyGame.Sort((x,y)=>DateTime.Compare(x.StartTime, y.StartTime));
            WeeklyPractice.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
            //set the earliest time in the week to game day
            if(weeklyGame.Count >= 1)
            {
                dashboard.GameDay = FormatGameName(weeklyGame[0], users);
            }
            else
            {
                dashboard.GameDay = "No Upcoming Games";
            }
            //set the earliest time practice in the week to practice time
            if (WeeklyPractice.Count >=1)
            {
                dashboard.PracticeTime = PracticeTypeFormat(WeeklyPractice[0]);
            }
            else
            {
                dashboard.PracticeTime = "No Practices Created";
            }
            // to Do  two different cards for players to see when they log in to the system
            dashboard.NumGames = weeklyGame.Count;
            dashboard.NumPractices = WeeklyPractice.Count;
            //store into session variable
            return dashboard;
        }
        //formats game name and practice type format
        public static string FormatGameName(Game game,Users user)
        {
            if(game is null)
            {
                return "No Upcoming Games";
            }
            else
            {
                Team teamHome = team.GetTeams().Find(m => m.TeamID == game.HomeTeam);
                Team teamAway = team.GetTeams().Find(m => m.TeamID == game.AwayTeam);


                if (teamHome.TeamID == user.TeamID)
                {
                    return "Home vs " + teamAway.TeamName.ToString();
                }
                else
                {
                    return "Away vs " + teamHome.TeamName.ToString();
                }
                
            }
        }
        public static string PracticeTypeFormat(Practice practice)
        {
            if(practice is null)
            {
                return "No Upcoming Practices";
            }
            else
            {
                return practice.PracticeType.ToString();
            }
        }

        //gets a string of random hexidecimals colors runs the hexidecimal random function for number of players on team 
        public static string[] RandomColor(int count)
        {
            //initalize array
            string[] colors = new string[count];
            //keep random instances out of loops!!!!!
            var r = new Random();

            for (int x = 0; x < count; x++)
            {
                colors[x] = HexidecimalRandom(r);
            }
            //check for duplicates
            //string [] randomColors=CheckDuplicates(colors);
            //return the populated list of strings
            return colors;
        }
        //returns a string of color hexidecimal

        //uses the random generated by random color to create a random hexidecimal color string to return 
        public static string HexidecimalRandom(Random r)
        {
            
            return string.Format("#{0:X6}", r.Next(0x1000000));

        }

    }
}
