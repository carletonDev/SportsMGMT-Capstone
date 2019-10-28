

namespace SportsMGMTCommon
{ //the class that instantiates instances of the home screen calculations
    using System;
    using System.Collections.Generic;
    public class DashBoard
    {
        public static NullDashboard Null = NullDashboard;
        private static NullDashboard NullDashboard { get => new NullDashboard(); }
        public string GameDay { get; set; }
        public string PracticeTime { get; set; }

        public int NumGames { get; set; }
        public int NumPractices { get; set; }
        public List<Users> MyRoster { get; set; }

        public List<Team> Standings { get; set; }
        public int NoTeam { get; set; }

        public int NoRoles { get; set; }

        public string Message { get; set; }

        public decimal CapSpace { get; set; }

        public decimal TeamSalary { get; set; }

        public decimal PercentageCap { get; set; }

        public string MyTeam { get; set; }

        public PlayerStats MyStats { get; set; }

        public decimal AverageSalary { get; set; }

        public DateTime ContractExpires { get; set; }
        public string DaysRemaining { get; set; }
        public int TeamWins { get; set; }

        public int TeamLosses { get; set; }

        public List<PlayerStats> PlayerStats { get; set; }

        public List<Users> FreeAgents { get; set; }
   


    }
    public class NullDashboard : DashBoard
    {
        public NullDashboard()
        {
            GameDay = "No GameDay";
            PracticeTime = "No Time";
            NumGames = 0;
            NumPractices = 0;
            MyRoster = new List<Users>();
            MyRoster.Add(Users.Null);
            MyTeam = Team.Null.TeamName;
            Standings = new List<Team>();
            Standings.Add(Team.Null);
            NoTeam = 0;
            MyStats = new NullPlayerStats();
            AverageSalary = 0.0M;
            ContractExpires = DateTime.Now;
            DaysRemaining = "No Contract Time";
            TeamWins = 0;
            TeamLosses = 0;
            PlayerStats = new List<PlayerStats>();
            PlayerStats.Add(new NullPlayerStats());
            FreeAgents = new List<Users>();
            FreeAgents.Add(Users.Null);
            NoRoles = 0;
            Message = "No New Messages";
            CapSpace = 0.0M;
            TeamSalary = 0.0M;
            PercentageCap = 0.0M;
            MyTeam = "No Team";


        }
    }
}
