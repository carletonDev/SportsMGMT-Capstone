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
