using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SportsMGMTApp.Models
{
    public class UserRegister
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please Enter a FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Please enter a Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }

        [EmailAddress(ErrorMessage="Invalid E-mail")]
        public string Email { get; set; }
        [Phone(ErrorMessage="Invalid Phone")]
        public string Phone { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage="User has exceeded maximum characters")]
        public string UserName { get; set; }
        [Required]
        //[MinLength(8, ErrorMessage = "Your password must have 8-20 characters")]
        //[MaxLength(20, ErrorMessage = "User has exceeded maximum characters")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "Password must have 8 ~ 20 characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }
    }
}