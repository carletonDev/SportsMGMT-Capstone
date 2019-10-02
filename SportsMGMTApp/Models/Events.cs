using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsMGMTCommon;
using System.Drawing;
using DHTMLX.Scheduler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsMGMTApp.Models
{
    public class EventGet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        public string Title { get; set; }
        [DHXJson(Alias = "start_date")]
        public DateTime Start { get; set; }
        [DHXJson(Alias = "end_date")]
        public DateTime End { get; set; }
        [DHXJson(Alias = "text")]
        public string Description { get; set; }

        //public Color StatusColor { get; set; }

        //public Color BackgroundColor { get; set; }

    }
}