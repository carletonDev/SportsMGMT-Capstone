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

        public int UserID { get; set; }

        public int TeamID { get; set; }

        public int ContractID { get; set; }

        public string FullName { get; set; }

        public bool ChangePassword { get; set; }
        //finds who midified the user last for formatting
        public static string WhoModified(int id)
        {
            UsersBLL usersBLL = new UsersBLL();

                string name = "";
            if (id!=0)
            {
                //the impossible scenario
                name =  usersBLL.GetUsers().Find(m => m.UserID == id).FullName;
            }
            else
            {
                name = "No Modifier";
            }
            return name;
        }
        //Format the Value of Team
        public static string TeamName(int id)
        {
            TeamBLL teamBLL = new TeamBLL();
            string Name = "";
            if (id == 0)
            {
                Name = "No Team";
            }
            else
            {
                Name = teamBLL.GetTeams().Find(m => m.TeamID == id).TeamName;
            }
           
            return Name;
        }
        //find role name
        public static string RoleName(int id)
        {
            RolesBLL rolesBLL = new RolesBLL();
            string Name ="";
            if (id == 0)
            {
                Name = "No Role";
            }
            else { Name = rolesBLL.GetRoles().Find(m => m.RoleID == id).RoleType; }
            return Name;
        }
        //finds the users team for formatting
        public IEnumerable<SelectListItem> GetTeams
        {
            get
            {
                TeamBLL getTeam = new TeamBLL();
                return new SelectList(getTeam.GetTeams(), "TeamID", "TeamName");
            }
        }


        public IEnumerable<SelectListItem> GetRoles { get {
                RolesBLL getRoles = new RolesBLL();
                return new SelectList(getRoles.GetRoles(), "RoleID", "RoleType");
            } }

        public IEnumerable<SelectListItem> GetContract {
            get {
                ContractsBLL contractsBLL = new ContractsBLL();
                return new SelectList(contractsBLL.GetContracts(), "ContractID", "ContractType");
            } }
        //Finds the name of the contracts
        public static string FindContractName(int id)
        {
            ContractsBLL contractsBLL = new ContractsBLL();
            string Name = "";
            if(id == 0)
            {
                Name = "No Contract";
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