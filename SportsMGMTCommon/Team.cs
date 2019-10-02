namespace SportsMGMTCommon
{
    public class Team
    {
        //create properties for the Database object Team
        public int TeamID { get; set; }
        public decimal SalaryCap { get; set; }
        public string TeamName { get; set; }
        public string TeamType { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
    }
}
