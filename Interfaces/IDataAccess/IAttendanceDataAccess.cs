using SportsMGMTCommon;
using System;
using System.Collections.Generic;

namespace Interfaces.IDataAccess
{
    public interface IAttendanceDataAccess
    {
        void CreateGameAttance(GameAttendance gameAttended);
        void CreatePracticeAttendance(PracticeAttended practiceAttended);
        List<GameAttendance> getGameAttendance();

        List<PracticeAttended> getPracticeAttendaned(int id);
    }
}
