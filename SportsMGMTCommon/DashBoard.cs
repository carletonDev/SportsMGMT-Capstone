

namespace SportsMGMTCommon
{ //the class that instantiates instances of the home screen calculations
    using System;
    using System.Collections.Generic;
    public class DashBoard
    {
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
}
