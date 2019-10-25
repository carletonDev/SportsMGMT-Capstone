

namespace SportsMGMTBLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interfaces.IBusinessLogic;
    using SportsMGMTCommon;
    using SportsMGMTDataAccess;
    public class PlayerStatsBLL:IPlayerStats
    {
        //CRUD BLL for PLAYER Stats
        public List<PlayerStats> GetStats()
        {
            PlayerStatsDA playerStatsDA = new PlayerStatsDA();
            List<PlayerStats> getStats = playerStatsDA.GetPlayerStats();
            return getStats;
        }
        public void InsertStats(PlayerStats player)
        {
            PlayerStatsDA playerStatsDA = new PlayerStatsDA();
            playerStatsDA.InsertPlayerStats(player);
        }
        public void UpdateStats(PlayerStats player)
        {
            PlayerStatsDA playerStatsDA = new PlayerStatsDA();
            playerStatsDA.UpdatePlayerStats(player);
        }
        public void DeleteStats(int id)
        {
            PlayerStatsDA playerStatsDA = new PlayerStatsDA();
            playerStatsDA.DeletePlayerStats(id);
        }
    }
}
