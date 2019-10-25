

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

    public class GameBLL:IGame
    {
        //CRUD BLL For Games
        public List<Game> GetGames()
        {
            GameDataAccess gameDataAccess = new GameDataAccess();
            List<Game> getGames = gameDataAccess.GetGames();
            return getGames;
        }
        public void CreateGame(Game game)
        {
            GameDataAccess gameDataAccess = new GameDataAccess();
            gameDataAccess.CreateGame(game);
        }
        public void DeleteGame(Game game)
        {
            GameDataAccess gameDataAccess = new GameDataAccess();
            gameDataAccess.DeleteGame(game);
        }
        public bool UpdateGame(Game game)
        {
            try
            {
                GameDataAccess gameDataAccess = new GameDataAccess();
                gameDataAccess.UpdateGame(game);
            }
            catch (Exception ex)
            {
                ExeceptionDataAccess ExceptionDA = new ExeceptionDataAccess();
                ExceptionDA.StoreExceptions(ex);
                return false;
            }
            return true;
        }


    }
}
