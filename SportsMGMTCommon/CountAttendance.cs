

namespace SportsMGMTCommon
{
    // A class for counting attendance for the bar graph on chart.js example page
    public  class CountAttendance
    {
        public static NullCountAttendance Null = NullCountAttendanceInst;
        private static NullCountAttendance NullCountAttendanceInst { get => new NullCountAttendance(); }
        public string FullName { get; set; }

        public int NumPresent { get; set; }

        public int NumAbsent { get; set; }

        public int total { get; set; }

        public decimal AbsentRatio { get; set; }

        public decimal PresentRatio { get; set; }

    }
    public class NullCountAttendance : CountAttendance
    {
        public NullCountAttendance() {
            FullName = "No Name";
            NumPresent = 0;
            NumAbsent = 0;
            total = 0;
            AbsentRatio = 0.0M;
            PresentRatio = 0.0M;
        }
    


    }
}
