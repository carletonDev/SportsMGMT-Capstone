using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IAttendanceBLL
    {
        void CreateGameAttance(GameAttendance gameAttended);
        void CreatePracticeAttendance(PracticeAttended practiceAttended);
        List<GameAttendance> getGameAttendaned();

        List<PracticeAttended> getPracticeAttendaned(int id);
    }
}
