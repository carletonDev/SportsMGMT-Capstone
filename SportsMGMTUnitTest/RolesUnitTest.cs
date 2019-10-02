using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTBLL;
using SportsMGMTCommon;
namespace SportsMGMTUnitTest
{
    [TestClass]
    public class RolesUnitTest
    {
        [TestMethod]
        public void GetRoles()
        {
            //Run a test that checks the list population count to see if it equals the database
            //Arrange
            RolesBLL rolesBLL = new RolesBLL();
            //Act
            List<Roles> getRoles = rolesBLL.GetRoles();
            //Roles roles =getRoles.(m => m.RoleType == "admin");
            int count = getRoles.Count;
            Roles role = getRoles.Find(m => m.RoleType == "Admin");
            //Assert
            Assert.IsTrue(role.RoleID ==1) ;
        }
        [TestMethod]
        public void InsertRole()
        {
            //Arrange
            RolesBLL rolesBLL = new RolesBLL();
            //Act
            Roles role = new Roles();
            role.RoleType = "TestInsert";
            rolesBLL.InsertRole(role);
            List<Roles> checkRole = rolesBLL.GetRoles();
            //Assert
            Assert.IsTrue(checkRole.Exists(m=>m.RoleType=="TestInsert"));
        }
        //Test if the check role access returns the users role
        [TestMethod]
        public void CheckRoleAccess()
        {
            //Arrange
            RolesBLL rolesBLL = new RolesBLL();
            Users user = new Users();
            user.FullName ="Carleton Cabarrus";
            //Act
            Roles role = rolesBLL.CheckRoleAccess(user);
            //Assert
            Assert.AreEqual("Admin", role.RoleType);
        }
        [TestMethod]
        public void DeleteRole()
        {
            //Arrange
            RolesBLL rolesBLL = new RolesBLL();
            //Act
            Roles role = new Roles();
            role.RoleType = "TestInsert";
            rolesBLL.DeleteRoles(role);
            List<Roles> checkRole = rolesBLL.GetRoles();
            //Assert
            Assert.IsFalse(checkRole.Exists(m => m.RoleType == "TestInsert"));
        }
        [TestMethod]
        public void UpdateRolesbyName()
        {
            //Arrange
            UsersBLL usersBLL = new UsersBLL();
            RolesBLL rolesBLL = new RolesBLL();
            //Act-Find Bob biggum and Coach role
            List<Roles> getRole = rolesBLL.GetRoles();
            Roles role = getRole.Find(m => m.RoleType == "Coach");
            List<Users> getUsers = usersBLL.GetUsers();
            Users user = getUsers.Find(m => m.FullName == "Bob Biggum");
            //Assign Bob Biggum new Role
            rolesBLL.UpdateRolesByName(user, role);
            //get new check list of users
            List<Users> checkUsers = usersBLL.GetUsers();
            //Find Bob Biggum again
            Users users=checkUsers.Find(m => m.FullName == "Bob Biggum");

            //Assert that Bob Biggum has been updated to coach role
            Assert.IsTrue(users.RoleID == role.RoleID);
        }
    }
}
