using SportsMGMTCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.IDataAccess
{
    public interface IGameDataAccess
    {
        List<Game> GetGames();
        void CreateGame(Game game);
        void UpdateGameScore(Game game);
        void DeleteGame(Game game);
        void UpdateGame(Game game);

    }
}
