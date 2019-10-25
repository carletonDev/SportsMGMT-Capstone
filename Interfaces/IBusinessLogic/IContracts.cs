using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IContracts
    {
        List<Contracts> GetContracts();
        bool CreateContracts(Contracts contract);
        bool UpdateContract(Contracts contract);
        bool DeleteContracts(Contracts contract);
    }
}
