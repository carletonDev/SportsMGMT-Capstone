

namespace SportsMGMTCommon
{
    using System;

    public class Game
    {
        //create properties for Game Database Object
        public int GameID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int HomeTeam { get; set; }
        public int AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
}
