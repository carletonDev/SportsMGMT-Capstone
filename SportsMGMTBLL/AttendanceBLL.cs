namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTDataAccess;
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;

    public class AttendanceBLL:IAttendanceBLL
    {
        IAttendanceDataAccess attendance;

        public AttendanceBLL(IAttendanceDataAccess access)
        {
            attendance = access;
        }
        //Inserts a new Game Attendance
        public void CreateGameAttance(GameAttendance gameAttended)
        {
            attendance.CreateGameAttance(gameAttended);
        }
        //Inserts a new Practice Attendance
        public void CreatePracticeAttendance(PracticeAttended practiceAttended)
        {
           attendance.CreatePracticeAttendance(practiceAttended);
        }
        public List<PracticeAttended> getPracticeAttendaned(int id)
        {
            List<PracticeAttended> practice = attendance.getPracticeAttendaned(id);
            return practice;
        }
        //Get a List of All users from that team that has or hasnt been assigned game attendance values
        public List<GameAttendance> getGameAttendaned()
        {
            List<GameAttendance> games = attendance.getGameAttendance();
            return games;
        }


    }
}
