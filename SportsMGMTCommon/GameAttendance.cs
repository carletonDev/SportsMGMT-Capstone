namespace SportsMGMTCommon
{
    public class GameAttendance
    {
        //Add properties to represent the database object GameAttendance
        public static NullGameAttendance Null = NullGameAttendance;
        private static NullGameAttendance NullGameAttendance { get=>new NullGameAttendance();}
        public int GameID { get; set; }
        public int UserID { get; set; }
        public bool Attended { get; set; }
    }
    public class NullGameAttendance:GameAttendance
    {
        public NullGameAttendance()
        {
            GameID = 0;
            UserID = 0;
            Attended = false;
        }
    }
}
