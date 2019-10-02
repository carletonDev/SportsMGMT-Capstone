using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsMGMTCommon;
using SportsMGMTApp.Models;
namespace SportsMGMTApp.Mapper
{
    public class PracticeMapper
    {
        //Make Conversion to BLL layer common class practice
        public Practice PracticeMap(PracticeModel practiceModel)
        {
            Practice practice = new Practice();
            practice.PracticeType = practiceModel.PracticeType;
            practice.StartTime = practiceModel.StartTime;
            practice.EndTime = practiceModel.EndTime;


            return practice;
        }
    }
}