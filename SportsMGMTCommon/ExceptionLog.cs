

namespace SportsMGMTCommon
{
    using System;

    public class ExceptionLog
    {

      
        //create properties for Database objects ExceptionLogging
        public int LogID { get; set; }
        public string Message { get; set; }
        public DateTime DateLogged { get; set; }
    }
}
