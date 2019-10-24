using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
   public interface IContractsDataAccess
    {
        List<Contracts> GetContracts();
        bool CreateContract(Contracts contract);
        bool UpdateContract(Contracts contract);
        bool DeleteContracts(Contracts contract);
    }
}
