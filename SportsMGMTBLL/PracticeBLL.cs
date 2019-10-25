

namespace SportsMGMTBLL
{
    using System.Collections.Generic;
    using SportsMGMTCommon;
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;

    public class PracticeBLL : IPractice
    {
        IPracticeDataAccess practiceData;

        public PracticeBLL(IPracticeDataAccess practice)
        {
            practiceData = practice;
        }
        //CRUD BLL for Practice
        public List<Practice> GetPractice()
        {
            List<Practice> getPractice = practiceData.GetPractice();
            return getPractice;
        }
        public void CreatePractice(Practice practice)
        {

            practiceData.CreatePractice(practice);
        }
        public void UpdatePractice(Practice practice)
        {
            practiceData.UpdatePractice(practice);
        }
        public void DeletePractice(Practice practice)
        {
            practiceData.DeletePracticeById(practice);
        }

    }
}
