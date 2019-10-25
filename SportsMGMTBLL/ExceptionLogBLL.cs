

namespace SportsMGMTBLL
{
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;
    using System;
    public class ExceptionLogBLL:IExceptionsBLL
    {
        //Create and Read Exceptions only Create actually implemented
        IExceptions ExceptionDA;

        public ExceptionLogBLL(IExceptions exceptions)
        {
            ExceptionDA = exceptions;
        }
        public bool StoreExceptions(Exception Ex)
        {
            //store into a bool variable for test purposes
            bool check=ExceptionDA.StoreExceptions(Ex);
            //return check
            return check;
        }
    }
}
