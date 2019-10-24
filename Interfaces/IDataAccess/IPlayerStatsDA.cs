using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IPlayerStatsDA
    {
        List<PlayerStats> GetPlayerStats();
        void InsertPlayerStats(PlayerStats player);
        void UpdatePlayerStats(PlayerStats player);
        void DeletePlayerStats(int id);
    }
}
