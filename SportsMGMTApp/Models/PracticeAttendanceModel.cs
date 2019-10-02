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
    public class PracticeAttendanceModel
    {
        [Required]
        public int PracticeID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public bool Attended { get; set; }

        public IEnumerable<SelectListItem> GetUsers(int id)
        {
                UsersBLL usersBLL = new UsersBLL();
                return new SelectList(usersBLL.GetUsers().FindAll(m => m.TeamID == id), "UserID", "FullName");

        }

        public static string GetUserName(int id)
        {
            UsersBLL usersBLL = new UsersBLL();
            Users user = new Users();
            string name = "";
            if (id != 0)
            {
                user = usersBLL.GetUsers().Find(m => m.UserID == id);
                name = user.FullName;
            }
            else
            {
                name = "NULL";
            }
            return name;
        }

        public static string FormatBool(bool value)
        {
            string format="";
            if (value)
            {
                format = "Present";
            }
            else
            {
                format = "Absent";
            }
            return format;
        }

        public static string FormatPracticeID(int id)
        {
            PracticeBLL practiceBLL = new PracticeBLL();

            Practice findPractice = practiceBLL.GetPractice().Find(m => m.PracticeID == id);

            return findPractice.PracticeType.ToString();
        }


    }
}