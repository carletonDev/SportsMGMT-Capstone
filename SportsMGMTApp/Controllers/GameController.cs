
namespace SportsMGMTApp.Controllers
{
    using SportsMGMTApp.Filters;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SportsMGMTApp.Models;
    using SportsMGMTApp.Mapper;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using System;
    using Interfaces.IBusinessLogic;
    using SportsMGMTBLL.IOC;

    public class GameController : Controller
    {
        IGame gameBLL;
        public GameController()
        {
            gameBLL = new GameBLL(Resolve.Game(), Resolve.Exceptions());
        }
        // Create Post
        [HttpPost]
        [MustBeLoggedIn]
        [MustBeInRole(Roles="Admin")]
        public ActionResult Game(GameModel game)
        {

            if (ModelState.IsValid)
            {
                if (game.StartTime == DateTime.MinValue || game.EndTime==DateTime.MinValue)
                {
                    ViewBag.Message = "DateTime Cannot be the Beggining of time";
                    return View(game);
                }
                else if (game.StartTime > game.EndTime)
                {
                    ViewBag.Message = "Game Start Time cannot be greater than end time";
                    return View(game);
                }
                else if (game.EndTime < game.StartTime)
                {
                    ViewBag.Message = "Game End Time cannot be less than start time";
                    return View(game);
                }
                //Create Map
                GameMapper mapGame = new GameMapper();
                Game addGame = mapGame.MapGame(game);
                
                if (addGame.HomeTeam == addGame.AwayTeam)
                {
                    ViewBag.Message = "Home Team Cannot Be the Same as Away Team?";
                    return View(game);
                }
                else
                {
                    gameBLL.CreateGame(addGame);
                }
                //check insert added
                List<Game> checkGame = gameBLL.GetGames();
                if (checkGame.Exists(m => m.StartTime == addGame.StartTime))
                {
                    ViewBag.Message = "Game Created!";
                }
                else
                {
                    ViewBag.Message = "Game Failed!";
                }

            }
            return View(game);

        }
        // Create Get
        [HttpGet]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult Game()
        {
            GameModel model = new GameModel();
            return View(model);
        }
        //Update Get
        [HttpGet]
        [MustBeLoggedIn]
        [MustBeInRole(Roles = "Admin")]
        //passes in the model the form action has a get for state changes
        public ActionResult UpdateGame(int id)
        {

            GameModel gameModel = new GameModel();
            List<Game> getGame = gameModel.GetGames();
            gameModel.game = getGame.Find(m => m.GameID == id);
            return View(gameModel);
        }
        //UpdatePost
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        //when user post Save it will call the BLL update function to update the database and reset the non update parameters to same values
        public ActionResult UpdateGame(GameModel model)
        {

            if (ModelState.IsValid)
            {
                bool check = gameBLL.UpdateGame(model.game);
                //Display Update successful if update occured
                if (check)
                {
                    ViewBag.Message = "Update Successful";
                }
                else
                {
                    ViewBag.Message = "Update Failed!";
                }

            }
            return View(model);
        }
        //Read Get
        [HttpGet]
        [MustBeLoggedIn]
        [MustBeInRole(Roles = "Admin,Coach,Player")]
        public ActionResult ListGame()
        {
            GameModel gameModel = new GameModel();
            List<Game> getGames = gameModel.GetGames();
            return View(getGames);
        }
        //Delete Get
        [MustBeInRole(Roles="Admin")]
        [MustBeLoggedIn]
        [HttpGet]
        public ActionResult DeleteGame(int id)
        {

            GameModel gameModel = new GameModel(); // create a new model
            List<Game> getGame = gameModel.GetGames(); //store a list of games
            gameModel.game = getGame.Find(m => m.GameID == id); //store in the model objects common game object the object from the list
            return View(gameModel);
        }
        //Delete Post
        [MustBeLoggedIn]
        [MustBeInRole(Roles="Admin")]
        [HttpPost]
        public ActionResult DeleteGame(GameModel model)
        {
           
                Game game = model.GetGames().Find(m => m.GameID == model.game.GameID);
                gameBLL.DeleteGame(model.game); // delete game by using the common object game in the model
                List<Game> checkGame = model.GetGames(); //get a list of games curently
                bool check = checkGame.Exists(m => m.GameID == model.game.GameID); //store in a bool if the game still exists thats deleted
                if (check) //if it still exists
                {
                    ViewBag.Message = "Delete Failed";
                }
                else if(check==false)
                {
                    ViewBag.Message = "Delete Successful";
                }
            else
            {

            }
                model.game = game;
            return View(model);
        }
    }
}