namespace SportsMGMTApp.Controllers
{
    using Interfaces.IBusinessLogic;
    using SportsMGMTApp.Filters;
    using SportsMGMTApp.Models;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    public class TeamController : Controller
    {
        //CRUD for Teams self explanatory method names with more detailed comments in the method
        ITeam teamBLL;
        public TeamController(ITeam team)
        {
            teamBLL = team;
        }
      [HttpGet]
      [MustBeInRole(Roles ="Admin")]
      public ActionResult CreateTeam()
        {
            return View();
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin")]
        public ActionResult CreateTeam(TeamModel team)
        {
            if (ModelState.IsValid)
            {

                //Map Data Annotations to Common object
                Team createTeam = new Team();
                createTeam.TeamName = team.TeamName;
                createTeam.SalaryCap = Convert.ToDecimal(team.SalaryCap);
                createTeam.TeamType = "basketball";
                //Insert New Team
                teamBLL.CreateTeam(createTeam);
                //Check if Team Exists
                bool check = teamBLL.GetTeams().Exists(m => m.TeamName == createTeam.TeamName);

                if (check)
                {
                    ViewBag.Message = "New Team Added!";
                }
                else
                {
                    ViewBag.Message = "Team Not Added";
                }
                

            }
            else
            {
                ViewBag.Message = "Invalid Team Entry";
                return View(team);
            }

            return Redirect("~/Team/ListTeam");
        }

        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult ListTeam()
        {


            //create a new Team List
            List<Team> getTeams = teamBLL.GetTeams();
            //return view with model of list of teams
            return View(getTeams);
        }
        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateTeam(int id)
        {


            //create a new team model with data annotations
            TeamModel team = new TeamModel();

            Team findTeam = teamBLL.GetTeams().Find(m => m.TeamID == id);

            //map team in database to the team model for data validation
            team.TeamID = findTeam.TeamID;
            team.TeamName = findTeam.TeamName;
            team.TeamType = findTeam.TeamType;
            team.SalaryCap = findTeam.SalaryCap;
            team.Wins = findTeam.Wins;
            team.Losses = findTeam.Losses;

            return View(team);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin")]
        public ActionResult UpdateTeam(TeamModel team)
        {
            if (ModelState.IsValid)
            {


                //find the object in database to check against the updates
                Team checkTeam = teamBLL.GetTeams().Find(m => m.TeamID == team.TeamID);
                Team updateTeam = new Team();
                //Map to store object
                updateTeam.TeamID = team.TeamID;
                updateTeam.TeamName = team.TeamName;
                updateTeam.SalaryCap = team.SalaryCap;
                updateTeam.TeamType = team.TeamType;
                updateTeam.Wins = team.Wins;
                updateTeam.Losses = team.Losses;

                //perform update
                teamBLL.UpdateTeam(updateTeam);

                //check if update worked
                Team compareTeam = teamBLL.GetTeams().Find(m => m.TeamID == team.TeamID);

                if (checkTeam == compareTeam)
                {
                    ViewBag.Message = "Update Failed";
                }
                else
                {
                    ViewBag.Message = "Update Successful";
                }
                
                
            }
            else
            {
                ViewBag.Message = "Model State Invalid";
            }

            return View(team);
        }

        [HttpGet]
        [MustBeInRole(Roles="Admin")]
        public ActionResult DeleteTeam(int id)
        {

            //Create a team model for the view
            TeamModel team = new TeamModel();
            Team deleteTeam = new Team();
            //find object
            deleteTeam = teamBLL.GetTeams().Find(m => m.TeamID == id);
            team.Team = deleteTeam;

            return View(team);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin")]
        public ActionResult DeleteTeam(TeamModel team)
        {

            //find object
            Team deleteTeam = teamBLL.GetTeams().Find(m => m.TeamID == team.TeamID);
            team.Team = deleteTeam;
            //perform Delete
            teamBLL.DeleteTeam(team.Team);

            //check if object still exists
            bool check = teamBLL.GetTeams().Exists(m => m.TeamID == team.Team.TeamID);

            if (check)
            {
                ViewBag.Message = "Delete Failed";
            }
            else
            {
                ViewBag.Message = "Delete Successful";
            }


            return View(team);
        }
    }
}