using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interfaces.IBusinessLogic;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTApp.Models
{
    public class ContractModel
    {
        IUser usersBLL;
        public ContractModel(IUser user)
        {
            usersBLL = user;
        }
        [Key]
        public int ContractID { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Contract Type Exceeds Max Length")]
        public string ContractType { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public Contracts Contract { get; set; }

        public static List<Contracts> GetContracts { get; set; }

        public IEnumerable<SelectListItem> GetUsers(int teamid)
        {

            return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == teamid), "UserID", "FullName");

        }
    }
}