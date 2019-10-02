using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsMGMTApp.Models
{
    public class VersusFormat
    {
        public int Id { get; set; }
        public string VersusMatch { get; set; }

        public bool HomeGame { get; set; }
        public bool AwayGame { get; set; }
    }
}