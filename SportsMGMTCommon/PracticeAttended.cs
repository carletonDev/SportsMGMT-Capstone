namespace SportsMGMTCommon
{
    public class PracticeAttended
    {
        //Add properties to represent the database object PracticeAttendance
        public static NullPracticeAttended Null = NullPracticeAttended;
            private static NullPracticeAttended NullPracticeAttended { get => new NullPracticeAttended(); }
        public int PracticeID { get; set; }
        public int UserID { get; set; }
        public bool Attended { get; set; }
    }
    public class NullPracticeAttended:PracticeAttended
    {
        public NullPracticeAttended()
        {
            PracticeID = 0;
            UserID = 0;
            Attended = false;
        }
    }
}

