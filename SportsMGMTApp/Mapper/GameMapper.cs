using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SportsMGMTCommon;
using SportsMGMTApp.Models;

namespace SportsMGMTApp.Mapper
{
    public class GameMapper
    {
        public Game MapGame(GameModel game)
        {
            Game mapGame = new Game();
            mapGame.StartTime = game.StartTime;
            mapGame.EndTime = game.EndTime;
            mapGame.HomeTeam = game.HomeTeam;
            mapGame.AwayTeam = game.AwayTeam;

            return mapGame;
        }
    }
}