

namespace SportsMGMTCommon
{
    using System;

    public class Game
    {
        public static  NullGame Null = NullGameInst;
        private static NullGame NullGameInst { get => new NullGame(); }
        //create properties for Game Database Object
        public int GameID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
    public class NullGame:Game
    {
        public NullGame()
        {
            GameID = 0;
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            HomeTeam = 0;
            AwayTeam = 0;
            HomeTeamScore = 0;
            AwayTeamScore = 0;
        }

    }
}
