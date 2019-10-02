using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class AttendaceUnitTest
    {
        [TestMethod]
        public void GetGameAttendance()
        {
            //Arrange
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            GameAttendance game = new GameAttendance();
            //Act
            game.Attended = true;
            game.GameID = 1;
            //Pass object to get list of users who attended the game
            List<Users>getUsers= attendanceBLL.getGameAttendance(game);
            //Assert is true user attended the gam
            Assert.IsTrue(getUsers.Exists(m => m.FullName == "Carleton Cabarrus"));
        }
        [TestMethod]
        public void GetPracticeAttended()
        {
            //Arrange
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            PracticeAttended practiceAttended = new PracticeAttended();
            //Act
            practiceAttended.Attended = true;
            practiceAttended.PracticeID = 1;
            List<Users> getUsers = attendanceBLL.getPracticeAttendance(practiceAttended);

            //Assert
            Assert.IsTrue(getUsers.Exists(m => m.FullName == "Carleton Cabarrus"));


        }
        [TestMethod]
        public void CreateGameAttendance()
        {
            //Arrange
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            GameAttendance game = new GameAttendance();
            //Act
            game.UserID = 1;
            game.Attended = true;
            game.GameID = 3;
            attendanceBLL.CreateGameAttance(game);
            List<Users> getUsers = attendanceBLL.getGameAttendance(game);
            //Assert
            Assert.IsTrue(getUsers.Exists(m => m.FullName == "Carleton Cabarrus"));
        }
        [TestMethod]
        public void CreatePracticeAttendance()
        {
            //Arrange
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            PracticeAttended practiceAttended = new PracticeAttended();
            //Act
            practiceAttended.PracticeID = 1;
            practiceAttended.UserID = 9;
            practiceAttended.Attended = true;
            attendanceBLL.CreatePracticeAttendance(practiceAttended);
            List<Users> getUsers = attendanceBLL.getPracticeAttendance(practiceAttended);
            //Assert
            Assert.IsTrue(getUsers.Exists(m => m.FullName == "Kory Walmsely"));


        }
        [TestMethod]
        public void ViewPracticeAttendance()
        {
            //Arrange
            AttendanceBLL attendance = new AttendanceBLL();
            //Act
            List <PracticeAttended> practice = attendance.getPracticeAttendaned(1);
            //Assert
            Assert.IsTrue(practice.Count > 0);
        }
        [TestMethod]
        public void ViewGameAttendance()
        {
            //Arrange
            AttendanceBLL attendance = new AttendanceBLL();
            //Act
            List<GameAttendance> games = attendance.getGameAttendaned();
            //Assert
            Assert.IsTrue(games.Count > 0);
        }
    }
}
