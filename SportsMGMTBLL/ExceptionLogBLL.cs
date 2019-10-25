

namespace SportsMGMTBLL
{
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ExceptionLogBLL:IExceptions
    {
        //Create and Read Exceptions only Create actually implemented
        public bool StoreExceptions(Exception Ex)
        {
            //create a Data access object to call the method store exceptions
            ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
            //store into a bool variable for test purposes
           bool check=ExceptionDA.StoreExceptions(Ex);
            //return check
            return check;
        }
    }
}
