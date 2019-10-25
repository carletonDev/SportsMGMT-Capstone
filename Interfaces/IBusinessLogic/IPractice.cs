using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
   public interface IPractice
    {
        List<Practice> GetPractice();
        void UpdatePractice(Practice practice);
        void DeletePractice(Practice practice);
        void CreatePractice(Practice practice);
    }
}
