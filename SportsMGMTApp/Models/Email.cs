using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsMGMTCommon;
using SportsMGMTBLL;
using System.Net.Mail;
using System.Net;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SportsMGMTApp.Models
{
    public class Email
    {
        public string FromEmail { get; set; }
        [Required]
        public string Message { get; set; }
        public string FromName { get; set; }
        [Required]
        public string Subject { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Action { get; set; }
        [Required]
        public string To { get; set; }
        public string cc { get; set; }

    }
}