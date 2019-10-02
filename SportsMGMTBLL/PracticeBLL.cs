

namespace SportsMGMTBLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    public class PracticeBLL
    {
        //CRUD BLL for Practice
        public List<Practice> GetPractice()
        {
            PracticeDataAccess practiceDA = new PracticeDataAccess();
            List<Practice> getPractice = practiceDA.GetPractice();
            return getPractice;
        }
        public void CreatePractice(Practice practice)
        {
            PracticeDataAccess practiceDA = new PracticeDataAccess();
            practiceDA.CreatePractice(practice);
        }
        public void UpdatePractice(Practice practice)
        {
            PracticeDataAccess practiceData = new PracticeDataAccess();
            practiceData.UpdatePractice(practice);
        }
        public void DeletePractice(Practice practice)
        {
            PracticeDataAccess practiceData = new PracticeDataAccess();
            practiceData.DeletePracticeById(practice);
        }

    }
}
