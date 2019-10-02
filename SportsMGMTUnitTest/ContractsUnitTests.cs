using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class ContractUnitTest
    {
        [TestMethod]
        public void GetContracts()
        {
            //Arrange
            ContractsBLL getContracts = new ContractsBLL();
            //Act
            List<Contracts> contractList = getContracts.GetContracts();
            //Assert
            Assert.IsTrue(contractList.Exists(m => m.ContractType == "rookie player"));

        }
        [TestMethod]
        public void StoreContracts()
        {
            //Arrange
            ContractsBLL getContracts = new ContractsBLL();
            //Act
            Contracts contracts = new Contracts();
            contracts.ContractType = "Test";
            contracts.Salary = Convert.ToDecimal(100.00);
            getContracts.CreateContracts(contracts);
            //Once the object Test is stored get a new list of the contracts
            List<Contracts> contractList = getContracts.GetContracts();
            //Assert
            //Check that the contract exists in the updated list of contracts
            Assert.IsTrue(contractList.Exists(m => m.ContractType == "Test"));
        }
        [TestMethod]
        public void DeleteContracts()
        {
            //Arrange
            ContractsBLL getContracts = new ContractsBLL();
            List<Contracts> contractList = getContracts.GetContracts();
            //Act
            //Store the test variable whereever it is in the database into an object
            Contracts contract = contractList.Find(m => m.ContractType == "Test");
            //delete the object
            getContracts.DeleteContracts(contract);
            List<Contracts> contractCheck = getContracts.GetContracts();
            //Assert it's false that the object still exists
            Assert.IsFalse(contractCheck.Exists(m => m.ContractType == "Test"));

        }
    }
}
