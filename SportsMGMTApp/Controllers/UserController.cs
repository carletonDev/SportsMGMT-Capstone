
namespace SportsMGMTApp.Controllers
{
    using DHTMLX.Scheduler;
    using DHTMLX.Scheduler.Data;
    using SportsMGMTApp.Filters;
    using SportsMGMTApp.Mapper;
    using SportsMGMTApp.Models;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Configuration;
    using Interfaces.IBusinessLogic;

    /// <summary>
    /// The controller for the majority of user CRUD
    /// </summary>
    public class UserController : Controller
    {
        //inject interfaces into controller here
        IUser userBLL;
        IRole rolesBLL;
        ITeam team;
        IGame gameBLL;
        IPractice practiceBLL;
        IContracts contractsBLL;
        public UserController(IUser user, IRole role, ITeam teams, IGame game, IPractice practice,IContracts contracts)
        {
            userBLL = user;
            rolesBLL = role;
            team = teams;
            gameBLL = game;
            practiceBLL = practice;
            contractsBLL = contracts;
        }
        //CRUD for user as well as sending Emails More detailed comments in method
        /// <summary>
        /// The GET for the User Register view 
        /// </summary>
        /// <returns> user register view to create user</returns>
        public ActionResult UserRegister()
        {
            //show view
            return View();
        }
        /// <summary>
        /// The POST for user Register that  creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns> redirects to Login page</returns>
        [HttpPost]
        public ActionResult UserRegister(UserRegister user)
        {
            bool check = false;
            if (ModelState.IsValid)
            {
                Mapper.UserMapper UserMap = new Mapper.UserMapper();
                //set the user variable through method in mapper class

                Users insert = UserMap.SendToBLL(user);


                //create a list object to store current list of users

                List<Users> checkUser = userBLL.GetUsers();

                //check to see if the User already exist in database
                //check if user already exists with that UserName for data validation purposes for my DA method

                if (checkUser.Exists(m => m.UserName == insert.UserName))
                {
                    ViewBag.Message = "User Already Exist with that UserName";
                    return View(user);
                }
                else if (checkUser.Exists(m => m.Email == insert.Email))
                {
                    ViewBag.Message = "User Already Exists with that E-mail";
                    return View(user);
                }
                else
                {
                    //Call BLL method to insert the new user from view model and store success into a bool variable
                    check = userBLL.InsertNewUser(insert);
                }


            }
            else
            {
                ViewBag.Message = "Invalid Entry!";
                return View(user);
            }

            return Redirect("~\\User\\Login");

        }
        /// <summary>
        /// The Get for the forgot Password VIEW
        /// </summary>
        /// <returns> the view </returns>
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            //return empty view to collect the username
            return View();
        }
        /// <summary>
        /// Posts the forgot password action
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPassword(string username)
        {

            //check if the user exists 
            var dash = Session["Dash"] as DashBoard;
            dash.Message = username;
                bool check = userBLL.GetUsers().Exists(m => m.UserName == username);

                if (check) //if the user exists
                {
                    //redirect them to the password change view
                    return Redirect("~\\User\\PasswordChange");
                }
                else
                {   //change the session object message to user does not exist and return view with the username
                    ViewBag.Message = "User Does Not Exist with that UserName";
                return View();
            }
        }
        [HttpGet]
        public ActionResult PasswordChange()
        {
         //store dashboard set properties from username into an object
        //find the user with the message username passed from Forgot Password view

        var dash = Session["Dash"] as DashBoard;
        Users user = userBLL.GetUsers().Find(m => m.UserName == dash.Message);
            //store the users information into a LoginUser Model
            LoginUser login = new LoginUser
            {
                UserName = user.UserName,
                Password = user.Password
            };


            //pass model into view
            return View(login);
        }
        [HttpPost]
       public  ActionResult PasswordChange(LoginUser users)
        {
            if (ModelState.IsValid)
            {   //create a user BLL instance

                //Store update object into a common class object
                Users updateUser = userBLL.GetUsers().Find(m => m.UserName == users.UserName);
                //Store a check to see if update successful
                Users checkUser = userBLL.GetUsers().Find(m => m.UserName == users.UserName);
                //set update user object pass word to the password passed in by the model
                updateUser.Password = users.Password;
                //perform update
                userBLL.UpdateUser(updateUser);
                //check if the user has been updated by searching for all matching passwords first then to filter duplicates by unique username
                Users checkMethod = userBLL.GetUsers().FindAll(m => m.Password == updateUser.Password).Find(m => m.UserName == updateUser.UserName);
                //if the objects are not the same as the original object
                if (checkUser != checkMethod)
                {
                    //Create a New Session Variable
                    var dash = Session["Dash"] as DashBoard;
                    //set message as password changed
                    ViewBag.Message = "Password changed";
                    //send email to the users e-mail address notifying them of the change to password
                    Email email = new Email
                    {
                        Message = ConfigurationManager.AppSettings.Get("Message"),
                        FromName = ConfigurationManager.AppSettings.Get("Name"),
                        Subject = ConfigurationManager.AppSettings.Get("Subject"),
                        UserName = ConfigurationManager.AppSettings.Get("UserName"),
                        To = updateUser.Email
                    };
                    //use the send emails method from the home controllers
                    HomeController.SendEmails(email);
                    //Redirect to Login to display session message that password has changed

                }
                else
                {
                    ViewBag.Message = "Update Failed";
                    return View(users);
                }

            }
            else
            {
                ViewBag.Message = "Invalid Entry";
                return View(users);
            }
            return Redirect(ConfigurationManager.AppSettings.Get("Action"));
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["Dash"] = new DashBoard(); 
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                //Call mapper object to mapp items to the database
                UserMapper userMapper = new UserMapper();
                Users login = userMapper.LoginBLLMapper(loginUser);
                //call BLL layer

                Users loginUsers = userBLL.GetUsersByUserName(login.UserName);
                //display login successful or failed due to input error
                //if (loginUsers.Password is null) //BUG: Database connectivity has been an issue here is a patch for users not edited by admin
                //{
                //    Session["Users"] = loginUsers;
                //    return Redirect("~/User/NewUserHome");
                //}
                if (loginUsers.Password !=loginUser.Password)
                {
                    ViewBag.Message = "Invalid UserName and/or Password";
                    return View(loginUser);
                }
                //Call roles BLL

                Roles role = rolesBLL.CheckRoleAccess(loginUsers);
                //DO NOT REMOVE
                Session["Roles"] = role.RoleType;
                Session["UserName"] = loginUsers.UserName;
                //DO NOT REMOVE
                Session["Users"] = loginUsers;
                if (loginUsers.RoleID != 0)
                {
                    Session["Dash"] = MeaningfulCalculation.ReturnDashBoard(loginUsers);
                }
                //display login successful or failed
                    if (loginUsers.RoleID == 0)
                    {
                        return Redirect("~/User/NewUserHome");
                    }
                    else if (role.RoleType == "Admin")
                    {
                        return Redirect("~/Home/DashboardAdmin");
                    }
                    else if (role.RoleType == "Player")
                    {
                        return Redirect("~/Home/DashBoardPlayer");
                    }
            }
            else
            {
                ViewBag.Message = "Login Failed!";
                return View();
            }

            return Redirect("~/Home/DashboardCoach");
        }
        [HttpGet]
        public ActionResult Calendar()
        {
            //start a new scheduler
            var scheduler = new DHXScheduler(this)
            {
                Skin = DHXScheduler.Skins.Material
            };
            //config the hour range days and week start with on the calendar
            scheduler.Config.first_hour = 1;
            scheduler.Config.last_hour = 24;
            //Dynamic loading of months
            scheduler.EnableDynamicLoading(SchedulerDataLoader.DynamicalLoadingMode.Month);
            scheduler.LoadData = true;
            scheduler.EnableDataprocessor = true;
            return View(scheduler);
        }
        //Get Data From Game and Practice combines into one list
        public ContentResult Data(DateTime from, DateTime to)
        {
            List<EventGet> calendar = CalendarGet();
            var apps = calendar.Where(e => e.Start < to && e.End >= from).ToList();
            return new SchedulerAjaxData(apps);
        }
        //Method that combines Game and practice into one
        private List<EventGet> CalendarGet()
        {
            //Set variables to populate list to store in List<Events>
            List<Practice> getPractice = new List<Practice>();
            List<Game> getGames = new List<Game>();
            //create a team bll to get team names



            List<Team> getTeam = team.GetTeams();
            var users = Session["Users"] as Users;
            //populate lists
            getPractice = practiceBLL.GetPractice().FindAll(m=>m.TeamID==users.TeamID);
            getGames = gameBLL.GetGames();
            //Add the Games to the list that matches the login users team id
            List<Game> MyTeamGames = new List<Game>();
            MyTeamGames = getGames.FindAll(m => m.HomeTeam == users.TeamID ).ToList();

            List<Game> MyAwayGames = getGames.FindAll(m => m.AwayTeam == users.TeamID).ToList();
            //Add practice to the list that matches the Login Users team ID
            //Create Event List
            List<EventGet> addEvents = new List<EventGet>();

            foreach (Practice practice in getPractice)
            {
                //populate an event object for each practice from the database to add to full calendar
                EventGet events = new EventGet();
                int practiceId = 001;
                events.EventID = practiceId;
                events.Title = "Practice";
                events.Description = practice.PracticeType;
                events.Start = practice.StartTime;
                events.End = practice.EndTime;
                //events.StatusColor = System.Drawing.Color.Green;
                //events.BackgroundColor = System.Drawing.Color.Aqua;
                addEvents.Add(events);
                practiceId++;

            }
            foreach (Game games in MyTeamGames)
            {

                //Get Team Name For Home and Away
                Team teamHome = getTeam.Find(m => m.TeamID == games.HomeTeam);
                Team teamAway = getTeam.Find(m => m.TeamID == games.AwayTeam);

                //populate the event object for Each Home Game
                EventGet events = new EventGet();
                int gameId = 100;
                events.EventID = gameId;
                events.Title = teamHome.TeamName + " " + "vs" + " " + teamAway.TeamName; ;
                events.Description = teamHome.TeamName + " Score:" + games.HomeTeamScore.ToString() + " " + teamAway.TeamName + " Score:" + games.AwayTeamScore.ToString();
                events.Start = games.StartTime;
                events.End = games.EndTime;
                //events.StatusColor = System.Drawing.Color.Orange;
                //events.BackgroundColor = System.Drawing.Color.Blue;
                addEvents.Add(events);
                gameId++;
            }
            foreach (Game games in MyAwayGames)
            {

                //Get Team Name For Home and Away
                Team teamHome = getTeam.Find(m => m.TeamID == games.HomeTeam);
                Team teamAway = getTeam.Find(m => m.TeamID == games.AwayTeam);

                //populate the event object for Each Away Game
                EventGet events = new EventGet();
                int gameId = 01;
                events.EventID = gameId;
                events.Title = teamHome.TeamName + " " + "vs" + " " + teamAway.TeamName; ;
                events.Description = teamHome.TeamName + " Score:" + games.HomeTeamScore.ToString() + " " + teamAway.TeamName + " Score:" + games.AwayTeamScore.ToString();
                events.Start = games.StartTime;
                events.End = games.EndTime;
                //events.StatusColor = System.Drawing.Color.Black;
                //events.BackgroundColor = System.Drawing.Color.Gold;
                addEvents.Add(events);
                gameId++;
            }
            return addEvents;
        }
        [HttpGet]
        public ActionResult NewUserHome()
        {
            return View();
        }
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult ListAllUsers()
        {

            var user = Session["Users"] as Users;
            List<Users> viewAll = new List<Users>();
            if (user.RoleID == 1)
            {
                viewAll = userBLL.GetUsers();
            }
            else if (user.RoleID == 2)
            {
                viewAll = userBLL.GetUsers().FindAll(m=>m.TeamID==user.TeamID);
                viewAll.AddRange(userBLL.GetUsers().FindAll(m => m.TeamID == 1034));
                viewAll.AddRange(userBLL.GetUsers().FindAll(m => m.TeamID == 0));
            }
            return View(viewAll);
        }

        [HttpPost]
        [MustBeInRole(Roles="Coach")]
        public ActionResult ListAllUsers(int id)
        {
            var user = Session["Users"] as Users;

            Users findUser = userBLL.GetUsers().Find(m => m.UserID == id);

            findUser.TeamID = 1034;

            userBLL.UpdateUser(findUser);
            List<Users> myTeam = userBLL.GetUsers().FindAll(m => m.TeamID == user.TeamID);
            bool check=myTeam.Exists(m => m.UserID == id);

            //check if player still exists in team roster
            if (check != true)
            {
                ViewBag.Message = "Player Removed from Team";
            }
            else
            {
                ViewBag.Message = "Player Was Not Removed";
            }
            return View(myTeam);
        }
        
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach,Player")]
        public ActionResult MyProfile()
        {
            //Make a new Register
            UserRegister user = new UserRegister();

            var users = Session["Users"] as Users; //store user session values into an object
            Users findUser = userBLL.GetUsers().Find(m => m.UserID == users.UserID); //find the user in the database
            string[] fullname = findUser.FullName.Split(' '); //spilt the fullname for form purposes

            //set values to show user in model to edit
            user.FirstName = fullname[0];//
            user.LastName = fullname[1]; //check
            user.Phone = findUser.Phone;//check
            user.Address = findUser.Address; //check
            user.Password = findUser.Password; //check
            user.Email = findUser.Email; //check
            user.UserName = findUser.UserName; //check
            user.UserID = findUser.UserID; //check

            return View(user);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin,Coach,Player")]
        public ActionResult MyProfile(UserRegister user)
        {
            if (ModelState.IsValid)
            {


                // object for checking edit and to store the edits from the user

                Users editCheck = userBLL.GetUsers().Find(m => m.UserID == user.UserID);
                Users findUser = userBLL.GetUsers().Find(m => m.UserID == user.UserID);

                //set values with data annotations

                findUser.FullName = user.FirstName + " " + user.LastName;
                findUser.UserName = user.UserName;
                findUser.Password = user.Password;
                findUser.Phone = user.Phone;
                findUser.Address = user.Address;
                findUser.Email = user.Email;

                userBLL.UpdateUser(findUser);
                //check if update successful and store messages
                if (editCheck == findUser)
                {
                    ViewBag.Message = "Update Failed";
                }
                else
                {
                    ViewBag.Message = "Update Success";
                }
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
            }
            return View(user);
        }

        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult DeleteUser(int id)
        {

            UserModel userModel = new UserModel();
            Users user = userBLL.GetUsers().Find(m => m.UserID == id);
            userModel.user = user;
            return View(userModel);
        }
        [HttpPost]
        [MustBeInRole(Roles = "Admin")]
        public ActionResult DeleteUser(UserModel model)
        {

                Users user = userBLL.GetUsers().Find(m => m.UserID == model.user.UserID);
                userBLL.DeleteUserByName(model.user);
                bool check = userBLL.GetUsers().Exists(m => m.UserID == model.user.UserID);
                if (check)
                {
                    ViewBag.Message = "Delete Failed";
                }
                else
                {
                    ViewBag.Message = "Delete Successful";
                }
            Email email = new Email
            {
                Message = ConfigurationManager.AppSettings.Get("DeleteMessage"),
                FromEmail = ConfigurationManager.AppSettings.Get("From"),
                UserName = ConfigurationManager.AppSettings.Get("UserName"),
                Password = ConfigurationManager.AppSettings.Get("Password"),
                Subject = "Account Deleted!",
                FromName = ConfigurationManager.AppSettings.Get("Name"),
                To = user.Email
            };
            HomeController.SendEmails(email);

            return View(model);
        }

        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateUser(int id)
        {

            Users user = userBLL.GetUsers().Find(m => m.UserID == id);

            UserModel player = new UserModel
            {
                user = user
            };

            return View(player);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateUser(UserModel model)
        {
           
            


            return View(model);
        }

        [HttpGet]
        [MustBeInRole(Roles="Coach,Admin")]
        public ActionResult UpdateTeam(int id)
        {
            UserModel user = new UserModel();



            Users finduser = userBLL.GetUsers().Find(m => m.UserID == id);

            user.UserID = finduser.UserID;
            user.FullName = finduser.FullName;
            user.Phone = finduser.Phone;
            user.Email = finduser.Email;


            return View(user);
        }
        [HttpPost]
        [MustBeInRole(Roles="Coach,Admin")]
        public ActionResult UpdateTeam(UserModel model)
        {

            Users finduser = userBLL.GetUsers().Find(m => m.UserID == model.UserID);

            //set the users team and contract to none
            finduser.TeamID = 1034;
            finduser.ContractID = 2017;

            userBLL.UpdateUser(finduser);
            //check if it worked
            bool check = userBLL.GetUsers().Exists(m => m.UserID == finduser.UserID && m.TeamID == 1034);

            if (check)
            {
                ViewBag.Message = "Player removed from team";
            }
            else if(check == false)
            {
                ViewBag.Message = "Player removal failed";
            }
            else
            {

            }

            return View(model);
        }

        public DashBoard ReturnDashBoard()
        {
            //Users
            var users = Session["Users"] as Users;
            //Instantiate the dashboard class for session variables

            DashBoard dashboard = new DashBoard();


            Team getTeam = new Team();
            List<Users> getUsers = new List<Users>();
  
            if (users.TeamID != 0)
            {
                getTeam = team.GetTeams().Find(m => m.TeamID == users.TeamID);
            }
            else { }
            //Get Average Contract
            if (users.TeamID != 0)
            {
                getUsers = userBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            }
            else { }

            List<decimal> salaries = new List<decimal>();
            //Find Days remaining till Contract Expiration

            //find when the contract expires
            if (users.ContractDuration != 0)
            {
                dashboard.ContractExpires = users.ContractStart.AddDays(users.ContractDuration * 365);
            }
            TimeSpan time = (dashboard.ContractExpires-DateTime.Now);
            string daysRemaining = time.Days.ToString() + "Days" + time.Hours.ToString() + "Hours" + time.Minutes.ToString() + "Minutes";
            dashboard.DaysRemaining = daysRemaining;
            // Add users contracts to salary to sum and average

            foreach (Users user in getUsers)
            {
                Contracts contracts = contractsBLL.GetContracts().Find(m => m.ContractID == user.ContractID);
                salaries.Add(contracts.Salary);
            }
            //Get average
            decimal averageSalary = salaries.Sum() / salaries.Count;
            //store in dashboard
            dashboard.AverageSalary = averageSalary;
            //User Messages and Alerts
            //message for users with no teams

            List<Users> getUser = userBLL.GetUsers().FindAll(m => m.TeamID == 0);
            getUser.AddRange(userBLL.GetUsers().FindAll(m => m.TeamID == 1034));
            dashboard.NoTeam = getUser.Count;

            dashboard.FreeAgents = getUser;

            //Get Users Roster for his Team
            dashboard.MyRoster = userBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            //Get Team Standings
            dashboard.Standings = team.GetTeams().FindAll(m => m.TeamType == "basketball");
            //message for users with no roles for admin to notifiy

            List<Users> UsersRole = userBLL.GetUsers().FindAll(m => m.RoleID == 0);
            dashboard.NoRoles = UsersRole.Count;

            //Salary Cap Remaining
            List<Users> FindCap = userBLL.GetUsers().FindAll(m => m.TeamID == users.TeamID);
            List<decimal> CapSpace = new List<decimal>();
            //for each user in find cap
            foreach(Users users1 in FindCap)
            {
                //get contract object and add salary times contract year to the capspace list
                Contracts salary=contractsBLL.GetContracts().Find(m => m.ContractID == users1.ContractID);
                CapSpace.Add(Convert.ToDecimal(salary.Salary * users1.ContractDuration));
            }
            //Sum all the contracts
            dashboard.CapSpace = CapSpace.Sum();
            dashboard.TeamSalary = team.GetTeams().Find(m => m.TeamID == users.TeamID).SalaryCap;
            var result = dashboard.CapSpace / dashboard.TeamSalary;
            dashboard.PercentageCap = 1 - result;
            //Display Win Loss ratio
            dashboard.TeamWins = team.GetTeams().Find(m => m.TeamID == users.TeamID).Wins;
            dashboard.TeamLosses = team.GetTeams().Find(m => m.TeamID == users.TeamID).Losses;
            //store into session variable
            return dashboard;
        }

    }
}