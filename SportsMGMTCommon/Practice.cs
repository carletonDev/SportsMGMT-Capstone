
namespace SportsMGMTCommon
{
    using System;
    public class Practice
    {
        //create Properties for the Database Object Practice
        public int PracticeID { get; set; }
        public string PracticeType { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TeamID { get; set; }
    }
}
