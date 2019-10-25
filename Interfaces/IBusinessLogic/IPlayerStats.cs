using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
    public interface IPlayerStats
    {
        List<PlayerStats> GetStats();
        void InsertStats(PlayerStats player);
        void UpdateStats(PlayerStats player);
        void DeleteStats(int id);
    }
}
