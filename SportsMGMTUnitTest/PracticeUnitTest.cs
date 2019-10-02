using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class PracticeUnitTest
    {
        [TestMethod]
        public void GetPractice()
        {
            //Arrange
            PracticeBLL practiceBLL = new PracticeBLL();
            //Act
            List<Practice> getPractice = practiceBLL.GetPractice();
            //Assert
            Assert.IsTrue(getPractice.Exists(m => m.PracticeType == "Scrimmage"));
        }
        [TestMethod]
        public void CreatePractice()
        {
            //Arrange
            PracticeBLL practiceBLL = new PracticeBLL();
            Practice practice = new Practice();
            //Act -- add weights
            practice.PracticeType = "Weights";
            practice.TeamID = 1;
            practiceBLL.CreatePractice(practice);
            List<Practice> getPractice = practiceBLL.GetPractice();
            //Assert--that the new object exist in database
            Assert.IsTrue(getPractice.Exists(m => m.PracticeType == "Weights"));
        }
        [TestMethod]
        public void UpdatePractice()
        {

            //Arrange
            PracticeBLL practiceBLL = new PracticeBLL();
            //Act -update practice to current date time for endtime
            List<Practice> getPractice = practiceBLL.GetPractice();
            Practice practice = getPractice.Find(m => m.PracticeType == "Weights");
            practice.EndTime = Convert.ToDateTime("6/28/2019 10:49:00 AM");
            practiceBLL.UpdatePractice(practice);
            List<Practice> checkPractice = practiceBLL.GetPractice();
            //Assert-- check if the end time is datetime now
            Assert.IsTrue(checkPractice.Exists(m => m.EndTime == Convert.ToDateTime("6/28/2019 10:49:00 AM")));
        }
        [TestMethod]
        public void DeletePractice()
        {
            //Arrange
            PracticeBLL practiceBLL = new PracticeBLL();
            //Act -delete practice based off newly found practice
            List<Practice> getPractice = practiceBLL.GetPractice();
            Practice practice = getPractice.Find(m => m.PracticeType == "Weights");
            practiceBLL.DeletePractice(practice);
            List<Practice> checkPractice = practiceBLL.GetPractice();
            //Assert -is false it doesnt exist
            Assert.IsFalse(checkPractice.Exists(m => m.EndTime == Convert.ToDateTime("6/28/2019 10:49:00 AM")));
        }
    }
}

