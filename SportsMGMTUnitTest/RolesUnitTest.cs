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
            //    //Run a test that checks the list population count to see if it equals the database
            //    //Arrange
            //    RolesBLL rolesBLL = new RolesBLL();
            //    //Act
            //    List<Roles> getRoles = rolesBLL.GetRoles();
            //    //Roles roles =getRoles.(m => m.RoleType == "admin");
            //    int count = getRoles.Count;
            //    Roles role = getRoles.Find(m => m.RoleType == "Admin");
            //    //Assert
            //    Assert.IsTrue(role.RoleID ==1) ;
            //}
        }
        //Test if the check role access returns the users role
        [TestMethod]
        public void CheckRoleAccess()
        {
            ////Arrange
            //RolesBLL rolesBLL = new RolesBLL();
            //Users user = new Users();
            //user.FullName ="Carleton Cabarrus";
            ////Act
            //Roles role = rolesBLL.CheckRoleAccess(user);
            ////Assert
            //Assert.AreEqual("Admin", role.RoleType);
        }
        [TestMethod]
        public void DeleteRole()
        {
            ////Arrange
            //RolesBLL rolesBLL = new RolesBLL();
            ////Act
            //Roles role = new Roles();
            //role.RoleType = "TestInsert";
            //rolesBLL.DeleteRoles(role);
            //List<Roles> checkRole = rolesBLL.GetRoles();
            ////Assert
            //Assert.IsFalse(checkRole.Exists(m => m.RoleType == "TestInsert"));
        }

    }
}
