﻿namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class MeaningfulCalculation : IMeaningfulCalculation
    {
        //Meaningful Calulation to count the number of Attendance Vs Absence per game 
        public static IAttendanceBLL attendance;
        public static IUser usersBLL;
        public static ITeam team;
        public static IContracts contractsBLL;
        public static IPractice practiceBLL;
        public static IGame games;
        public MeaningfulCalculation(IAttendanceBLL attend, IUser user, ITeam teamBLL, IContracts contracts, IPractice practice, IGame game)
        {
            attendance = attend;
            usersBLL = user;
            team = teamBLL;
            contractsBLL = contracts;
            practiceBLL = practice;
            games = game;
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
        public DashBoard ReturnDashBoard(Users users)
        {
            //Users
            //var users = Session["Users"] as Users;
            //Instantiate the dashboard class for session variables

            DashBoard dashboard = new DashBoard();
            Team getTeam = MyTeam(users);
            //find when the contract expires
            ContractExpires(users, dashboard);
            // Add users contracts to salary to sum and average
            AverageSalary(dashboard, GetUsers(users));
            //User Messages and Alerts
            //message for users with no teams
            Team freeAgent = NoTeamNotifications(dashboard);
            MyRoster(users, dashboard);
            //Get Team Standings
            TeamStandings(dashboard);
            //message for users with no roles for admin to notifiy

            FindNoRoles(dashboard);

            //Salary Cap Remaining
            GetCapSpace(dashboard, FindCap(users));
            FindSalaryCap(users, dashboard);
            decimal result = GetPercentageCap(dashboard);
            dashboard.PercentageCap = 1 - result;
            //Display Win Loss ratio
            dashboard.TeamWins = GetWins(users);
            dashboard.TeamLosses = GetLosses(users);
            MyTeamName(users, dashboard, freeAgent);
            //find all upcoming games and practices for the next two weeks for notifications for the players


            List<Game> homeGames = GetHomeGames(users);
            List<Game> awayGame = GetAwayGame(users);
            List<Game> allGames = AllGames(homeGames, awayGame);
            List<Practice> teamPractices = PracticeByUser(users);
            //check time span find the game one week away
            List<Game> weeklyGame = (from Game game in allGames
                                     let gameTime = game.StartTime - DateTime.Now
                                     where gameTime.Days >= 1
                                     select game).ToList();
            //same thing for all practices
            List<Practice> WeeklyPractice = (from Practice practice in teamPractices
                                             let practiceTime = practice.StartTime - DateTime.Now
                                             where practiceTime.Days >= 1
                                             select practice).ToList();
            //Finds nearest gametime
            GameTime(users, dashboard, weeklyGame);
            PracticeTime(users, dashboard, WeeklyPractice);
            // to Do  two different cards for players to see when they log in to the system
            dashboard.NumGames = weeklyGame.Count;
            dashboard.NumPractices = WeeklyPractice.Count;
            //store into session variable
            return dashboard;
        }

        private static List<Users> FindCap(Users users)
        {
            return usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
        }

        private static List<Users> GetUsers(Users users)
        {
            List<Users> getUsers = new List<Users>();
            //Get Average Contract
            if (users.TeamID != 0)
            {
                getUsers = usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            }
            else { }

            return getUsers;
        }

        private static Team NoTeamNotifications(DashBoard dashboard)
        {
            Team freeAgent = team.GetTeams().Find(m => m.TeamName == Team.Null.TeamName);
            List<Users> getUser = usersBLL.GetUsers().FindAll(m => m.TeamID == Team.Null.TeamID);
            getUser.AddRange(usersBLL.GetUsers().FindAll(m => m.TeamID == freeAgent.TeamID));
            dashboard.NoTeam = getUser.Count;

            dashboard.FreeAgents = getUser;
            return freeAgent;
        }

        private static void MyRoster(Users users, DashBoard dashboard)
        {

            //Get Users Roster for his Team
            dashboard.MyRoster = usersBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
        }

        private static void TeamStandings(DashBoard dashboard)
        {
            dashboard.Standings = team.GetTeams().FindAll(m => m.TeamType == "basketball");
            //sort by wins
            dashboard.Standings.Sort((x, y) => y.Wins.CompareTo(x.Wins));
        }

        private static void FindNoRoles(DashBoard dashboard)
        {
            List<Users> UsersRole = usersBLL.GetUsers().FindAll(m => m.RoleID == Roles.Null.RoleID);
            dashboard.NoRoles = UsersRole.Count;
        }

        private static void GetCapSpace(DashBoard dashboard, List<Users> FindCap)
        {
            List<decimal> CapSpace = new List<decimal>();
            //for each user in find cap
            foreach (Users users1 in FindCap)
            {
                //get contract object and add salary times contract year to the capspace list
                Contracts salary = contractsBLL.GetContracts().Find(m => m.ContractID == users1.ContractID);
                if (salary == null)
                {
                    salary = Contracts.Null;
                }
                CapSpace.Add(Convert.ToDecimal(salary.Salary * users1.ContractDuration));
            }
            //Sum all the contracts
            dashboard.CapSpace = CapSpace.Sum();
        }

        private static void FindSalaryCap(Users users, DashBoard dashboard)
        {
            dashboard.TeamSalary = team.GetTeams().Find(m => m.TeamID == users.TeamID).SalaryCap;
        }

        private static decimal GetPercentageCap(DashBoard dashboard)
        {
            return dashboard.TeamSalary != Team.Null.SalaryCap ? dashboard.CapSpace / dashboard.TeamSalary : Team.Null.SalaryCap;
        }

        private static int GetWins(Users users)
        {
            return team.GetTeams().Find(m => m.TeamID == users.TeamID).Wins;
        }

        private static int GetLosses(Users users)
        {
            return team.GetTeams().Find(m => m.TeamID == users.TeamID).Losses;
        }

        private static List<Game> GetHomeGames(Users users)
        {
            return games.GetGames().FindAll(m => m.HomeTeam == users.TeamID);
        }

        private static List<Game> GetAwayGame(Users users)
        {
            return games.GetGames().FindAll(m => m.AwayTeam == users.TeamID);
        }

        public void MyTeamName(Users users, DashBoard dashboard, Team freeAgent)
        {
            if (users.TeamID != freeAgent.TeamID)
            {
                dashboard.MyTeam = team.GetTeams().Find(m => m.TeamID == users.TeamID).TeamName;
            }
            else
            {
                dashboard.MyTeam = Team.Null.TeamName;
            }
        }

        public Team MyTeam(Users users)
        {
            return users.TeamID != Team.Null.TeamID ? team.GetTeams().Find(m => m.TeamID == users.TeamID) : Team.Null;
        }

        public List<Game> AllGames(List<Game> homeGames, List<Game> awayGame)
        {
            List<Game> allGames = new List<Game>();
            //store all users games into the game table
            allGames.AddRange(homeGames);
            allGames.AddRange(awayGame);
            //if the games are null set to null singleton
            if (allGames == null)
            {
                allGames.Add(Game.Null);
            }

            return allGames;
        }

        public List<Practice> PracticeByUser(Users users)
        {
            //Get all practices
            List<Practice> teamPractices = practiceBLL.GetPractice().FindAll(m => m.TeamID == users.TeamID);
            //if practice are null set to null singleton
            if (teamPractices == null)
            {
                teamPractices.Add(Practice.Null);
            }

            return teamPractices;
        }

        public void GameTime(Users users, DashBoard dashboard, List<Game> weeklyGame)
        {
            //sort weekly game
            weeklyGame.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
            //set the earliest time in the week to game day
            if (weeklyGame.Count >= 1)
            {
                dashboard.GameDay = FormatGameName(weeklyGame[0], users);
            }
            else
            {
                dashboard.GameDay = DashBoard.Null.GameDay;
            }
        }
        public void PracticeTime(Users users, DashBoard dashboard, List<Practice> WeeklyPractice)
        {
            WeeklyPractice.Sort((x, y) => DateTime.Compare(x.StartTime, y.StartTime));
            //set the earliest time practice in the week to practice time
            if (WeeklyPractice.Count >= 1)
            {
                dashboard.PracticeTime = PracticeTypeFormat(WeeklyPractice[0]);
            }
            else
            {
                dashboard.PracticeTime = DashBoard.Null.PracticeTime;
            }
        }

        public void ContractExpires(Users users, DashBoard dashboard)
        {
            if (users.ContractDuration != 0)
            {
                dashboard.ContractExpires = users.ContractStart.AddDays(users.ContractDuration * 365);
            }
            TimeSpan time = (dashboard.ContractExpires - DateTime.Now);
            string daysRemaining = time.Days.ToString() + " Days, " + time.Hours.ToString() + " Hours, " + time.Minutes.ToString() + " Minutes";
            dashboard.DaysRemaining = daysRemaining;
        }

        public void AverageSalary(DashBoard dashboard, List<Users> getUsers)
        {
            List<decimal> salaries = new List<decimal>();
            foreach (Users user in getUsers)
            {
                Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == user.ContractID);
                if (contracts == null)
                {
                    contracts = Contracts.Null;
                }
                if (contracts.Salary != Contracts.Null.Salary)
                {
                    salaries.Add(contracts.Salary);
                }
                else { contracts.Salary = Contracts.Null.Salary; }
            }
            //Get average
            decimal averageSalary = 0.0M;
            if (salaries.Count >= 1)
            {
                averageSalary = salaries.Sum() / salaries.Count;
            }
            //store in dashboard
            dashboard.AverageSalary = averageSalary;
        }

        //formats game name and practice type format
        public string FormatGameName(Game game, Users user)
        {
            if (game is null)
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
        public string PracticeTypeFormat(Practice practice)
        {
            if (practice is null)
            {
                return "No Upcoming Practices";
            }
            else
            {
                return practice.PracticeType.ToString();
            }
        }

        //gets a string of random hexidecimals colors runs the hexidecimal random function for number of players on team 
        public string[] RandomColor(int count)
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
        public string HexidecimalRandom(Random r)
        {

            return string.Format("#{0:X6}", r.Next(0x1000000));

        }

    }
}
