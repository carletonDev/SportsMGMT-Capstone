using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IPracticeDataAccess
    {
        List<Practice> GetPractice();
        void UpdatePractice(Practice practice);
        void DeletePracticeById(Practice practice);
        void CreatePractice(Practice practice);
    }
}
