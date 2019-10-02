namespace SportsMGMTCommon
{
    //Class that creates player statistics objects for database
    public class PlayerStats
    {
        public int StatID { get; set; }

        public int UserID { get; set; }

        public int GameID { get; set; }
        public int TeamID { get; set; }

        public int Points { get; set; }

        public int Assists { get; set; }

        public int Rebounds { get; set; }

        public int Misses { get; set; }
    }
}
