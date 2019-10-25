using Interfaces.IBusinessLogic;
using SportsMGMTBLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsMGMTApp.Models
{
    public class PlayerStatsModel
    {
        IUser usersBLL;
        public PlayerStatsModel(IUser user)
        {
            usersBLL = user;

        }
        public IEnumerable<SelectListItem> GetUsers(int teamid)
        {

            return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == teamid), "UserID", "FullName");

        }
        public int StatID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int GameID { get; set; }
        [Required]
        public int Points { get; set; }
        [Required]
        public int Assists { get; set; }
        [Required]
        public int Rebounds { get; set; }
        [Required]
        public int Misses { get; set; }
        public int TeamID { get;  set; }
    }
}