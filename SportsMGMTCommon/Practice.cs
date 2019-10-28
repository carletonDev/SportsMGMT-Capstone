
namespace SportsMGMTCommon
{
    using System;
    public class Practice
    {
        //create Properties for the Database Object Practice
        public static NullPractice Null = NullPractice;
        private static NullPractice NullPractice { get => new NullPractice(); }
        public int PracticeID { get; set; }
        public string PracticeType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeamID { get; set; }
    }
    public class NullPractice:Practice
    {
        public NullPractice()
        {
            PracticeID = 0;
            PracticeType = "No Practice";
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            TeamID = 0;
        }
    }
}
