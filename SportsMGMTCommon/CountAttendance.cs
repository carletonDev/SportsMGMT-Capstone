

namespace SportsMGMTCommon
{
    // A class for counting attendance for the bar graph on chart.js example page
    public  class CountAttendance
    {
        public string FullName { get; set; }

        public int NumPresent { get; set; }

        public int NumAbsent { get; set; }

        public int total { get; set; }

        public decimal AbsentRatio { get; set; }

        public decimal PresentRatio { get; set; }

    }
}
