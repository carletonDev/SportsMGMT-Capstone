using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IBusinessLogic
{
   public interface IGame
    {
        List<Game> GetGames();
        void CreateGame(Game game);
        void DeleteGame(Game game);
        bool UpdateGame(Game game);
    }
}
