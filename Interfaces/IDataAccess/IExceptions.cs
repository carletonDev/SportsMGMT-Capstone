using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IExceptions
    {
        bool StoreExceptions(Exception ex);
    }
}
