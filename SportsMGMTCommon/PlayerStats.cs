namespace SportsMGMTCommon
{
    //Class that creates player statistics objects for database
    public class PlayerStats
    {
        public static NullPlayerStats Null = NullPlayerStats;
        private static NullPlayerStats NullPlayerStats { get => new NullPlayerStats(); }
        public int StatID { get; set; }

        public int UserID { get; set; }

        public int GameID { get; set; }
        public int TeamID { get; set; }

        public int Points { get; set; }

        public int Assists { get; set; }

        public int Rebounds { get; set; }

        public int Misses { get; set; }
    }
    public class NullPlayerStats : PlayerStats
    {
        public NullPlayerStats()
        {
            StatID = 0;
            UserID = 0;
            GameID = 0;
            TeamID = 0;
            Points = 0;
            Assists = 0;
            Rebounds = 0;
            Misses = 0;
        }
    }
}
