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
        //Get the List of Users Who Have Either Attended or havent attended games
        public List<Users> getGameAttendance(GameAttendance gameAttendance)
        {
            AttendanceDataAccess attendanceDataAccess = new AttendanceDataAccess();
            List<Users> getGameAttendance = attendanceDataAccess.getGameAttendance(gameAttendance);
            return getGameAttendance;

        }
        //Gets the list of users who have attended or havent attended practice
        public List<Users> getPracticeAttendance(PracticeAttended practiceAttendance)
        {
            AttendanceDataAccess attendanceDataAccess = new AttendanceDataAccess();
            List<Users> getPracticeAttendance = attendanceDataAccess.getPracticeAttendance(practiceAttendance);
            return getPracticeAttendance;

        }
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
