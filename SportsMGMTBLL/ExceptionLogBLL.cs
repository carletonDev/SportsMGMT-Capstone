

namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class ExceptionLogBLL
    {
        //Create and Read Exceptions only Create actually implemented
        public List<ExceptionLog> GetExceptions()
        {
            //create a data access object to call the method Get exceptions to populate a list
            ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();

            List<ExceptionLog> listExceptions = ExceptionDA.GetExecptions();

            return listExceptions;
        }
        public bool StoreExceptions(Exception Ex)
        {
            //create a Data access object to call the method store exceptions
            ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
            //store into a bool variable for test purposes
           bool check=ExceptionDA.StoreExceptions(Ex);
            //return check
            return check;
        }
        public List<ExceptionLog> GetExceptionsById(int id)
        {
            //create a data access object to call the method Get exceptions to populate a list
            ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();

            List<ExceptionLog> listExceptions = ExceptionDA.GetExceptionbyId(id);

            return listExceptions;

        }
    }
}
