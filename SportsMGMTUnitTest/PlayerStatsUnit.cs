using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsMGMTBLL;
using SportsMGMTCommon;

namespace SportsMGMTUnitTest
{
    [TestClass]
    public class PlayerStatsunit
    {
        [TestMethod]
        public void ReadStats()
        {
            ////Arrange
            //PlayerStatsBLL playerStatsBLL = new PlayerStatsBLL();
            ////Act
            //List<PlayerStats> getStats = playerStatsBLL.GetStats();
            ////Assert
            //Assert.IsTrue(getStats.Count >= 1);
        }
        [TestMethod]
        public void CreateStats()
        {
            ////Arrange
            //PlayerStatsBLL playerStatsBLL = new PlayerStatsBLL();
            //PlayerStats player = new PlayerStats();
            ////Act
            //player.UserID = 2;
            //player.GameID = 1;
            //player.Misses = 10;
            //player.Points = 10;
            //player.Rebounds = 10;
            //player.Assists = 10;
            //playerStatsBLL.InsertStats(player);

            //List<PlayerStats> getStats = playerStatsBLL.GetStats();
            //PlayerStats checkPlayer = getStats.Find(m => m.UserID == 2);

            ////Assert
            //Assert.IsTrue(player.UserID == checkPlayer.UserID);

        }
        [TestMethod]
        public void UpdateStats()
        {
            ////Arrange
            //PlayerStatsBLL playerStatsBLL = new PlayerStatsBLL();
            ////Act
            //List<PlayerStats> getStats = playerStatsBLL.GetStats();
            //PlayerStats updatePlayer = getStats.Find(m => m.UserID == 2);
            //updatePlayer.Assists = 20;
            //playerStatsBLL.UpdateStats(updatePlayer);

            //List<PlayerStats> checkStats = playerStatsBLL.GetStats();
            //PlayerStats checkPlayer = checkStats.Find(m => m.Assists==20);
            ////Assert
            //Assert.IsTrue(checkPlayer.Assists == 20);
        }
        [TestMethod]
        public void DeleteStats()
        {
            ////Arrange
            //PlayerStatsBLL playerStatsBLL = new PlayerStatsBLL();
            //PlayerStats player = new PlayerStats();
            ////Act
            //List<PlayerStats> getStats = playerStatsBLL.GetStats();
            //PlayerStats updatePlayer = getStats.Find(m => m.UserID == 2);
            //playerStatsBLL.DeleteStats(updatePlayer.StatID);
            //bool check = playerStatsBLL.GetStats().Exists(m => m.StatID == updatePlayer.StatID);
            //Assert.IsFalse(check);

        }

    }
}
