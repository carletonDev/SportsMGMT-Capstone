using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsMGMTApp.Models
{
    public class LoginUser
    {
        [Required]
        [MaxLength(10,ErrorMessage ="Username exceeds max length")]
        public string UserName { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [MaxLength(20,ErrorMessage ="Password Exceeds Max Length")]
        public string Password { get; set; }
    }
}