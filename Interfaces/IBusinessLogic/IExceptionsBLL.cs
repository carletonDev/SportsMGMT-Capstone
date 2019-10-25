using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IExceptionsBLL
    {
        bool StoreExceptions(Exception ex);
    }
}
