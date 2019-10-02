
namespace SportsMGMTApp.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SportsMGMTApp.Models;
    using SportsMGMTApp.Filters;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    public class AttendanceController : Controller
    {
        // GET: Attendance
        [HttpGet]
        [MustBeInRole(Roles="Coach,Admin")]
        public ActionResult ViewGameAttendance()
        {
            
            AttendanceBLL attendance = new AttendanceBLL();
            UsersBLL usersBLL = new UsersBLL();
            var userInfo = Session["Users"] as Users;
            List<GameAttendance> games = attendance.getGameAttendaned();
            //get the list of users for only that team
            List<Users> users = usersBLL.GetUsers().FindAll(m => m.TeamID == userInfo.TeamID);
            //create a new list to store attendance for only that team
            List<GameAttendance> viewAttendanceByTeam = new List<GameAttendance>();
            //for each user in the coaches team
            foreach(Users user in users)
            {
                //find the users attendance
                GameAttendance game = games.Find(m => m.UserID == user.UserID);
                //add to list that sorts attendance by team
                viewAttendanceByTeam.Add(game);
            }
            return View(viewAttendanceByTeam);
        }
        //List Attendance for creation of new attendance
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult ListAttendance()
        {
            var users = Session["Users"] as Users;
            PracticeBLL practiceBLL = new PracticeBLL();
            List<Practice> getAllPractice = practiceBLL.GetPractice().FindAll(m => m.TeamID == users.TeamID);


            return View(getAllPractice);
        }
        //list attendance for creation of game *To implement later
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult ListGameAttendance()
        {
            var users = Session["Users"] as Users;
            GameBLL gameBLL = new GameBLL();
            List<Game> getAllGame = gameBLL.GetGames().FindAll(m => m.AwayTeam == users.TeamID);
            getAllGame.AddRange(gameBLL.GetGames().FindAll(m => m.HomeTeam == users.TeamID));

            return View(getAllGame);
        }
        //list practice attendance
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult ListPracticeAttendance()
        {
            var Users=Session["Users"] as Users;
            AttendanceBLL attendanceBLL = new AttendanceBLL();
            List<PracticeAttended> practice = attendanceBLL.getPracticeAttendaned(Users.TeamID);

            return View(practice);
        }
        //create game attendance
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult CreateGameAttendance(int id)
        {
            GameAttendanceModel model = new GameAttendanceModel
            {
                GameID = id
            };

            return View(model);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult CreateGameAttendance(GameAttendanceModel model)
        {
            if (ModelState.IsValid)
            {
                AttendanceBLL attendance = new AttendanceBLL();
                var user = Session["Users"] as Users;
                GameAttendance gameAttendance = new GameAttendance
                {
                    GameID = model.GameID,
                    UserID = model.UserID,
                    Attended = model.Attended
                };

                List<GameAttendance> duplicateCheck = attendance.getGameAttendaned();
                bool duplicateEntry = duplicateCheck.Exists(m => m.UserID == model.UserID && m.GameID == model.GameID);
                if (duplicateEntry)
                {
                    ViewBag.Message = "User already has attendance recorded";
                    return View(model);
                }
                else
                {
                    attendance.CreateGameAttance(gameAttendance);
                }

                //check if insert added
                List<GameAttendance> check = attendance.getGameAttendaned();
                bool verify = check.Exists(m => m.UserID == model.UserID);

                if (verify)
                {
                    ViewBag.Message = "Attendance Added";

                }
                else
                {
                    ViewBag.Message = "Creation Failed";
                }
            }
            else
            {
                ViewBag.Message = "Model State is not Valid";
                return View(model);
            }

            return View(model);
        }
        //create practice attendance
        [HttpGet]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult CreatePracticeAttendance(int id)
        {
            PracticeAttendanceModel model = new PracticeAttendanceModel
            {
                PracticeID = id
            };

            return View(model);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult CreatePracticeAttendance(PracticeAttendanceModel model)
        {
            if (ModelState.IsValid)
            {
                AttendanceBLL attendance = new AttendanceBLL();
                var user = Session["Users"] as Users;
                PracticeAttended practice = new PracticeAttended
                {
                    PracticeID = model.PracticeID,
                    UserID = model.UserID,
                    Attended = model.Attended
                };


                List<PracticeAttended> check = attendance.getPracticeAttendaned(user.TeamID);
                bool duplicateEntry = check.Exists(m => m.UserID == model.UserID && m.PracticeID == model.PracticeID);
                if (duplicateEntry)
                {
                    ViewBag.Message = "User Already has been entered for this practice";
                    return View(model);
                }
                else
                {
                    attendance.CreatePracticeAttendance(practice);
                }

                //check if insert added
                List<PracticeAttended> checkAgain = attendance.getPracticeAttendaned(user.TeamID);
                bool verify = checkAgain.Exists(m => m.UserID == model.UserID && m.PracticeID==model.PracticeID);

                if (verify)
                {
                    ViewBag.Message = "Attendance Added";

                }
                else
                {
                    ViewBag.Message = "Creation Failed";
                }
            }
            else
            {
                ViewBag.Message = "Model State is not Valid";
                return View(model);
            }

            return View(model);
        }
    }
}