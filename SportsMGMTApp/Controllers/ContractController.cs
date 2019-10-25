namespace SportsMGMTApp.Controllers
{
    using SportsMGMTApp.Filters;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using SportsMGMTApp.Models;
    using Interfaces.IBusinessLogic;
    using SportsMGMTBLL.IOC;

    public class ContractController : Controller
    {
        IContracts contractsBLL;
        IUser users;
        public ContractController(IContracts contracts, IUser user)
        {
            contractsBLL = contracts;
            users = user;
        }
        //list all contracts
        [HttpGet]
        [MustBeInRole(Roles ="Admin,Coach")]
        public ActionResult ListContracts()
        {


            List<Contracts> getContracts = new List<Contracts>();

            //store List of Teams in Model
            getContracts = contractsBLL.GetContracts();

            //send to view


            return View(getContracts);
        }
        //update get with id table only
        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateContracts(int id)
        {
        
            //create model
            ContractModel contract = new ContractModel(users);

            contract.Contract = contractsBLL.GetContracts().Find(m => m.ContractID == id);

            contract.ContractID = contract.Contract.ContractID;
            contract.ContractType = contract.Contract.ContractType;
            contract.Salary = contract.Contract.Salary;

            return View(contract);
        }
        //post update of contract
        [HttpPost]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateContracts(ContractModel contract)
        {
            if (ModelState.IsValid)
            {
                //set Variables to object
                Contracts contractUpdate = new Contracts();
                contractUpdate.ContractID = contract.ContractID;
                contractUpdate.ContractType = contract.ContractType;
                contractUpdate.Salary = contract.Salary;
                Contracts contractCheck = contractsBLL.GetContracts().Find(m => m.ContractID == contract.ContractID);
                contractsBLL.UpdateContract(contractUpdate);

                Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == contract.ContractID);

                //compare if update successful

                if (contracts != contractCheck)
                {
                    ViewBag.Message = "Update Successful";
                }
                else
                {
                    ViewBag.Message = "Update Failed";
                }
            }
            else
            {
                ViewBag.Message = "Model State Not Valid";
            }
            return View(contract);
        }
        //create contract
        [HttpGet]
        [MustBeInRole(Roles="Coach,Admin")]
        public ActionResult CreateContract()
        {
            return View();
        }
        //creates a new contract on post
        [HttpPost]
        [MustBeInRole(Roles = "Coach,Admin")]
        public ActionResult CreateContract(ContractModel contract)
        {
            if (ModelState.IsValid)
            {

                Contracts createContract = new Contracts();
                createContract.ContractType = contract.ContractType;
                createContract.Salary = contract.Salary;
                contractsBLL.CreateContracts(createContract);

                bool check = contractsBLL.GetContracts().Exists(m => m.ContractType == createContract.ContractType);

                if (check)
                {
                    ViewBag.Message = "Contract Created";
                }
                else
                {
                    ViewBag.Message = "Contract not Created";
                }
                    
            }
            else
            {
                ViewBag.Message = "Model State Not Valid";
            }

            return View(contract);
        }
        //deletes contract on id off table only
        [HttpGet]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult DeleteContract(int id)
        {


            Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == id);

            ContractModel contractModel = new ContractModel(users);

            contractModel.ContractID = contracts.ContractID;
            contractModel.ContractType = contracts.ContractType;
            contractModel.Salary = contracts.Salary;

            return View(contractModel);
        }

        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult DeleteContract(ContractModel contract)
        {

            //Find Object
            Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == contract.ContractID);
            //Delete Object
            contractsBLL.DeleteContracts(contracts);
            //Check if it exists still
            bool check = contractsBLL.GetContracts().Exists(m => m.ContractID == contract.ContractID);
            //if it does 
            if (check)
            {
                //delete failed
                ViewBag.Message = "Delete Failed";
            }
            else
            {
                ViewBag.Message = "Delete Successful";
            }

            return View(contract);
        }


    }
}