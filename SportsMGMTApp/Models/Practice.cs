using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTApp.Models
{
    public class PracticeModel
    {
        [MaxLength(100,ErrorMessage ="Max characters exceeded")]
        public string PracticeType { get; set; }
        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Enter Appropriate Start-time")]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public Practice practice { get; set; }
        public IEnumerable<SelectListItem> GetUsers(int teamid)
        {
            UsersBLL usersBLL = new UsersBLL();

            return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == teamid), "UserID", "FullName");

        }
        public int UserID { get; set; }
        public bool Check { get; set; }


    }
}