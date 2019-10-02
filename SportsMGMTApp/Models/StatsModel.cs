namespace SportsMGMTApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using SportsMGMTCommon;
    using SportsMGMTBLL;
    using System.Drawing;
    using System.Collections;
    using System.Web.Mvc;

    public class StatsModel
    {
        public List<CountAttendance> Count { get; set; }

        public List<double> Data { get; set; }

        public ArrayList xValue { get; set; }

        public ArrayList onlyPlayers { get; set; }

        public int[] Present { get; set; }

        public int[] Absent { get; set; }
        public int[] Average { get; set; }

        public int[] AverageAbsent { get; set; }

        public decimal[] PointsPerGame { get; set; }
        public int[] Points { get; set; }

        public int[] Assists { get; set; }

        public int[] Rebounds { get; set; }

        public int[] Misses { get; set; }

        public int[] GamePointAverage { get; set; }
        public int[] GameMissAverage { get; set; }
        public int[] GameAssistsAverage { get; set; }

        public int[] GameReboundAverage { get; set; }

        public decimal[]PiePoints{get;set;}
        public decimal[] PieAssists { get; set; }
        public decimal[] PieRebounds { get; set; }

        public string[] ColorRandom { get; set; }
    }
}