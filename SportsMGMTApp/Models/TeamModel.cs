using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SportsMGMTCommon;

namespace SportsMGMTApp.Models
{
    public class TeamModel
    {
     
        [Key]
        public int TeamID { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal SalaryCap { get; set; }
        [Required]
        [MaxLength(20,ErrorMessage ="Max Characters Allowed")]
        public string TeamName { get; set; }
        public string TeamType { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

       public List<Team> ListTeams { get; set; }

        public Team Team { get; set; }
    }
}