namespace SportsMGMTCommon
{
    public class Team
    {
        //create properties for the Database object Team
        public static readonly NullTeam Null = NullInst;
        private static NullTeam NullInst{ get { return new NullTeam(); } }
        public int TeamID { get; set; }
        public decimal SalaryCap { get; set; }
        public string TeamName { get; set; }
        public string TeamType { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
    public class NullTeam : Team
    {
       public NullTeam()
        {
            TeamID = 0;
            SalaryCap = 0.0M;
            TeamName = "Free Agent";
            TeamType = "No Type";
            Wins = 0;
            Losses = 0;
        }
    }
}
