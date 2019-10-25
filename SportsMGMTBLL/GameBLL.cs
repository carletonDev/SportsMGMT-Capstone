

namespace SportsMGMTBLL
{
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SportsMGMTDataAccess;
    using Interfaces.IBusinessLogic;
    using Interfaces.IDataAccess;

    public class GameBLL:IGame
    {
        IGameDataAccess gameDataAccess;
        IExceptions ExceptionDA;

        public GameBLL(IGameDataAccess game, IExceptions exceptions)
        {
            gameDataAccess = game;
            ExceptionDA = exceptions;
        }
        //CRUD BLL For Games
        public List<Game> GetGames()
        {

            List<Game> getGames = gameDataAccess.GetGames();
            return getGames;
        }
        public void CreateGame(Game game)
        {

            gameDataAccess.CreateGame(game);
        }
        public void DeleteGame(Game game)
        {

            gameDataAccess.DeleteGame(game);
        }
        public bool UpdateGame(Game game)
        {
            try
            {

                gameDataAccess.UpdateGame(game);
            }
            catch (Exception ex)
            {
                ExceptionDA.StoreExceptions(ex);
                return false;
            }
            return true;
        }


    }
}
