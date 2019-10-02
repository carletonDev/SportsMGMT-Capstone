using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTCommon;
using SportsMGMTBLL;
using System.Collections.Generic;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class UserUnitTests
    {
        [TestMethod]
        public void GetUsersByUserName()
        {
            //Arrange
            string username = "cabarruscl";
            UsersBLL usersBLL = new UsersBLL();
            //Act
            Users Users = usersBLL.GetUsersByUserName(username);
            
            //Assert
            Assert.AreEqual("Carleton Cabarrus",Users.FullName);
        }
        [TestMethod]
        public void GetUsers()
        {
            //Arrange
            UsersBLL usersBLL = new UsersBLL();
            //Act
            List<Users> Users = usersBLL.GetUsers();
            bool check = Users.Exists(m=>m.FullName== "SamuelHayes");
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void InsertUser()
        {
            //Arrange

            Users users = new Users();
            users.FirstName = "Mr";
            users.LastName = "Test";
            users.Address = "Test Street";
            users.Email = "Test@test.com";
            users.Phone = "8046606600";
            users.UserName = "Testing";
            users.Password = "pass1234";
            //Act
            UsersBLL usersBLL = new UsersBLL();
            bool check =usersBLL.InsertNewUser(users);
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void GetExceptions()
        {
            //Arrange
            ExceptionLogBLL ExceptionBLL = new ExceptionLogBLL();
            //Act
            List<ExceptionLog> exceptionLogs = ExceptionBLL.GetExceptions();
            ExceptionLog exceptionLog = new ExceptionLog();
            bool check=exceptionLogs.Exists(m =>m.Message == "testing the database ");
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void StoreExceptions()
        {
            //Arrange
            Exception ex = new Exception();
            ExceptionLogBLL exceptionLogBLL = new ExceptionLogBLL();
            //Act
            bool check = exceptionLogBLL.StoreExceptions(ex);
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void GetExceptionsById()
        {
            //Arrange
            ExceptionLogBLL ExceptionBLL = new ExceptionLogBLL();
            //Act
            List<ExceptionLog> exceptionLogs = ExceptionBLL.GetExceptionsById(1);
            bool check=exceptionLogs.Exists(m => m.LogID == 0);
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void ViewUsersOnTeam()
        {

            //Arrange
            int id = 1;
            bool check = false;
            UsersBLL usersBLL = new UsersBLL();
            //Act
            List<Users> myTeam=usersBLL.ViewMyTeam(id);
            check = myTeam.Exists(m => m.FullName == "Carleton Cabarrus");
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void ViewNullContracts()
        {
            //Arrange 
            UsersBLL usersBLL = new UsersBLL();
            List<Users> viewNullContracts = new List<Users>();
            //Act
            viewNullContracts = usersBLL.ViewNullContracts();
            bool check = viewNullContracts.Exists(m => m.FullName == "Mr test");
            //Assert
            Assert.IsFalse(check);
        }

        [TestMethod]
        public void AssignContracts()
        {
            //Arrange
            UsersBLL usersBLL = new UsersBLL();
            Users users = new Users();
            bool check;
            //Act
            users.FullName = "Bobby Messina";
            users.ContractID = 1;
            check=usersBLL.AssignContracts(users);
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void DeletePlayers()
        {
            //Arrange
            UsersBLL usersBLL = new UsersBLL();
            Users users = new Users();
            bool check;
            //Act
            users.FullName = "Bobby Messina";
            check = usersBLL.DeleteUserByName(users);
            //Assert
            Assert.IsTrue(check);
        }
        [TestMethod]
        public void UpdateUser()
        {
            //Arrange
            UsersBLL usersBLL = new UsersBLL();
            Users users = new Users();
            bool check;
            //Act 
            users.UserID = 1;
            users.TeamID = 1;
            users.ContractID = 1;
            users.FullName = "Carleton Cabarrus";
            users.UserModified = 1;
            users.RoleID = 1;
            users.Address = "65233 Sunset Dr";
            users.Email = "carleton519@gmail.com";
            users.Phone = "8045497611";
            users.UserName = "cabarruscl";
            users.Password = "nc23112";
            users.InjuryStatus = false;
            users.InjuryDescription = "Not Injured";

            check = usersBLL.UpdateUser(users);

            //Assert
            Assert.IsTrue(check);
        }
    }
}
