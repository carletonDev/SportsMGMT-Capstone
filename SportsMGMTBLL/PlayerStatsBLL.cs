

namespace SportsMGMTBLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    public class PlayerStatsBLL:IPlayerStats
    {
        //CRUD BLL for PLAYER Stats
        IPlayerStatsDA playerStatsDA;
        public PlayerStatsBLL(IPlayerStatsDA playerStats)
        {
            playerStatsDA = playerStats;
        }
        public List<PlayerStats> GetStats()
        {

            List<PlayerStats> getStats = playerStatsDA.GetPlayerStats();
            return getStats;
        }
        public void InsertStats(PlayerStats player)
        {
            playerStatsDA.InsertPlayerStats(player);
        }
        public void UpdateStats(PlayerStats player)
        {

            playerStatsDA.UpdatePlayerStats(player);
        }
        public void DeleteStats(int id)
        {

            playerStatsDA.DeletePlayerStats(id);
        }
    }
}
