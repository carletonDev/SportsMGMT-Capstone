namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTDataAccess;
    public class AttendanceBLL
    {

        //Inserts a new Game Attendance
        public void CreateGameAttance(GameAttendance gameAttended)
        {
            AttendanceDataAccess attendanceDataAccess = new AttendanceDataAccess();
            attendanceDataAccess.CreateGameAttance(gameAttended);
        }
        //Inserts a new Practice Attendance
        public void CreatePracticeAttendance(PracticeAttended practiceAttended)
        {
            AttendanceDataAccess attendanceDataAccess = new AttendanceDataAccess();
            attendanceDataAccess.CreatePracticeAttendance(practiceAttended);
        }
        public List<PracticeAttended> getPracticeAttendaned(int id)
        {
            AttendanceDataAccess attendance = new AttendanceDataAccess();
            List<PracticeAttended> practice = attendance.getPracticeAttendaned(id);
            return practice;
        }
        //Get a List of All users from that team that has or hasnt been assigned game attendance values
        public List<GameAttendance> getGameAttendaned()
        {
            AttendanceDataAccess attendance = new AttendanceDataAccess();
            List<GameAttendance> games = attendance.getGameAttendance();
            return games;
        }


    }
}
