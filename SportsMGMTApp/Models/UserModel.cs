using Interfaces.IBusinessLogic;
using SportsMGMTBLL;
using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsMGMTApp.Models
{
    public class UserModel
    {
        static IUser usersBLL;
        static ITeam teamBLL;
        static IRole rolesBLL;
        static IContracts contractsBLL;
        public UserModel(IUser user,ITeam teams,IRole role,IContracts contracts)
        {
            usersBLL = user;
            teamBLL = teams;
            rolesBLL = role;
            contractsBLL = contracts;
        }

        public int UserID { get; set; }

        public int TeamID { get; set; }

        public int ContractID { get; set; }

        public string FullName { get; set; }

        public bool ChangePassword { get; set; }
        //finds who midified the user last for formatting
        public  string WhoModified(int id)
        {
                string name = "";
            if (id!=0)
            {
                //the impossible scenario
                name =  usersBLL.GetUsers().Find(m => m.UserID == id).FullName;
            }
            else
            {
                name = "No modified";
            }
            return name;
        }
        //Format the Value of Team
        public  string TeamName(int id)
        {

            string Name = "";
            if (id == Users.Null.UserID)
            {
                Name = Team.Null.TeamName;
            }
            else
            {
                Name = teamBLL.GetTeams().Find(m => m.TeamID == id).TeamName;
            }
           
            return Name;
        }
        //find role name
        public  string RoleName(int id)
        {
            string Name ="";
            if (id == Users.Null.UserID)
            {
                Name = Roles.Null.RoleType;
            }
            else { Name = rolesBLL.GetRoles().Find(m => m.RoleID == id).RoleType; }
            return Name;
        }
        //finds the users team for formatting
        public IEnumerable<SelectListItem> GetTeams
        {
            get
            {

                return new SelectList(teamBLL.GetTeams(), "TeamID", "TeamName");
            }
        }


        public IEnumerable<SelectListItem> GetRoles { get {
                return new SelectList(rolesBLL.GetRoles(), "RoleID", "RoleType");
            } }

        public IEnumerable<SelectListItem> GetContract {
            get {
                return new SelectList(contractsBLL.GetContracts(), "ContractID", "ContractType");
            } }
        //Finds the name of the contracts
        public  string FindContractName(int id)
        {
            string Name = "";
            if(id == Contracts.Null.ContractID)
            {
                Name = Contracts.Null.ContractType;
            }
            else 
            {
                Name = contractsBLL.GetContracts().Find(m => m.ContractID == id).ContractType; 
            }
            return Name;
        }

        public int UserModified { get; set; }
   
        public int RoleID { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public bool InjuryStatus { get; set; }
        public string InjuryDescription { get; set; }
        public int ContractDuration { get; set; }

        public DateTime ContractStart { get; set; }
        [Required]
        public Users user { get; set; }

        
    }
}