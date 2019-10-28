
namespace SportsMGMTApp.Controllers
{
    using SportsMGMTApp.Models;
    using SportsMGMTBLL;
    using SportsMGMTCommon;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web.Mvc;
    using System.Web.Security;
    using System;
    using System.Net.Mail;
    using System.Net;
    using SportsMGMTApp.Filters;
    using System.Text;
    using Interfaces.IBusinessLogic;
    using SportsMGMTBLL.IOC;
    using Interfaces.IDataAccess;

    public class HomeController : Controller
    {
        IUser userBLL;
        ITeam teamBLL;
        IRole rolesBLL;
        IContracts contracts;
       static IExceptionsBLL exceptionLog;
        public HomeController(IUser user,ITeam team,IRole roles,IExceptionsBLL ex,IContracts contract)
        {
            userBLL = user;
            teamBLL = team;
            rolesBLL = roles;
            exceptionLog = ex;
            contracts = contract;
        }
        [MustBeLoggedIn]
        public ActionResult Index()
        {
            return View();
        }
        //returns the home page for admins with a model value of all users with no contracts
        [MustBeLoggedIn]
        [MustBeInRole(Roles="Admin")]
        public ActionResult DashboardAdmin()
        {
            //store the record for free agents and no contracts to list
            Team FreeAgent = teamBLL.GetTeams().Find(m => m.TeamName == "Free Agent");
            Contracts NoContract = contracts.GetContracts().Find(m => m.ContractType == "No Contract");
            //find all users with no teams or are on the free agent list
            List<Users> getUsers = userBLL.GetUsers().FindAll(m => m.ContractID == Contracts.Null.ContractID).FindAll(m => m.TeamID == Team.Null.TeamID);
            getUsers.AddRange(userBLL.GetUsers().FindAll(m => m.ContractID == NoContract.ContractID).FindAll(m => m.TeamID == FreeAgent.TeamID));

            return View(getUsers);
        }
        //returns the home page of coaches  with coach layout same model passed of users with not teams
        [MustBeLoggedIn]
        [MustBeInRole(Roles="Coach")]
        public ActionResult DashboardCoach()
        {
            //store the record for free agents and no contracts to list
            Team FreeAgent = teamBLL.GetTeams().Find(m => m.TeamName == "Free Agent");
            Contracts NoContract = contracts.GetContracts().Find(m => m.ContractType == "No Contract");
            //find all users with no teams or are on the free agent list
            List<Users> getUsers = userBLL.GetUsers().FindAll(m => m.ContractID == Contracts.Null.ContractID).FindAll(m => m.TeamID == Team.Null.TeamID);
            getUsers.AddRange(userBLL.GetUsers().FindAll(m => m.ContractID == NoContract.ContractID).FindAll(m => m.TeamID == FreeAgent.TeamID));
            return View(getUsers);
        }

        //dashboard for players which has team roster and current standings
        public ActionResult DashBoardPlayer()
        {
            return View();
        }
        //Email function in dashboard to email teammates and coaches
        [HttpGet]
        public ActionResult Email(string Email)
        {
            Email email = new Email();
            email.To = Email;
            return View(email);
        }
        
        [HttpPost]
        //Email Function to Email TeamMates and Coaches
        public ActionResult Email(Email email)
        {
            var user = Session["Users"] as Users;
            var dashboard = Session["Dash"] as DashBoard;
            if (ModelState.IsValid)
            {
                
                email.FromName = user.FullName; //store users name in from name section
                email.FromEmail = ConfigurationManager.AppSettings.Get("From"); //set the admin email sender in from
                email.UserName = ConfigurationManager.AppSettings.Get("UserName"); //set the username to admin sender
                email.Password = ConfigurationManager.AppSettings.Get("Password"); //set password to admin sender
                 SendEmails(email);
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
                return View(email);
            }
            //return the user back to their home screen based on role after email and confirmation email has been sent
            if(user.RoleID == (int)Role.Admin)
            {
               return Redirect("~/Home/DashboardAdmin");
            }
            else if(user.RoleID == (int)Role.Coach)
            {
               return Redirect("~/Home/DashboardCoach");
            }
            else
            {
               return Redirect("~/Home/DashboardPlayer");
            }
        }
        //logout action from button
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }
        //update user view that dynamically changes based on role
        [HttpGet]
        [MustBeInRole(Roles = "Coach,Admin")]
        public ActionResult AssignContract(int id)
        {
            //Instantiate a new users BLL and find the user to modify store in the model the user who has modified the users table from session obj

            Users user = userBLL.GetUsers().Find(m => m.UserID == id);
            //create a new user model
            UserModel userModel = new UserModel(userBLL,teamBLL,rolesBLL,contracts);
            //place the user from id into user model
            userModel.user = user;
            return View(userModel);
        }
        [HttpPost]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult AssignContract(UserModel model)
        {

            if (ModelState.IsValid)
            {
                //instantiate a user bll object

                //find the original user information
                Users user = userBLL.GetUsers().Find(m => m.UserID == model.user.UserID);
             
                //store the modifying user in a var
                var users = Session["Users"] as Users;
                //if the user has requested Admin to change his password due to security issues
                if (model.ChangePassword)
                {
                    model.user.Password = Users.Null.Password;
                }
                else
                {

                }
                //if username is null
                if (user.UserName is null)
                {
                    model.user.UserName = Users.Null.UserName;
                }
                else
                {

                }
                //if role is admin
                if (users.RoleID != (int)Role.Admin)
                {
                    model.user.UserModified = users.UserID; //set who modified the user last
                    model.user.TeamID = users.TeamID; //assign him to coach team
                }
                else
                {
                    model.user.UserModified = users.UserID; //set who modified user last
                    if(model.TeamID == Team.Null.TeamID)
                    {
                        model.user.TeamID = teamBLL.GetTeams().Find(m => m.TeamName == Team.Null.TeamName).TeamID;
                    }
                    else
                    {
                        int id = model.TeamID;
                        model.user.TeamID = id;
                    }
                }
                if (model.user.RoleID != SportsMGMTCommon.Roles.Null.RoleID)
                {
                    //do nothing to the users role if it has been assigned
                }
                else if (users.RoleID != (int)Role.Admin)
                {

                    model.user.RoleID = rolesBLL.GetRoles().Find(m => m.RoleType == "Player").RoleID;
                }
                if(model.user.ContractStart == DateTime.MinValue)
                {
                    model.user.ContractStart = DateTime.Now;
                }
                model.user.FullName = user.FullName;
                //update the user
                if (model.user == user)
                {
                    ViewBag.Message = "You have already updated the user with these values";
                    return View(model);
                }
                userBLL.UpdateUser(model.user);
                //check if update successful
                if (user == model.user) // if the model user is still the same as user before modify
                {
                    ViewBag.Message = "Update Failed"; //display update failed
                }
                else
                {
                    ViewBag.Message = "Update Successful"; // display update success
                }
            }
            else
            {
                ViewBag.Message = "Invalid Data Entry";
            }
            return View(model);
        }
        //the email sending function all email sending uses
        public static void SendEmails(Email email)
        {
            
            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>"; //structure the body of the message
            //Get the Host settings and port settings for Yahoo
            string smtpAddress = ConfigurationManager.AppSettings.Get("Host");
            int portNumber = 587;
            bool enableSSL = true;
            string emailFrom = ConfigurationManager.AppSettings.Get("From");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string emailTo = email.To;
            string subject = email.Subject;
            var message= string.Format(body, email.FromName, email.FromEmail, email.Message);

            try
            {

                // Create a Client and Send Message

                SmtpClient smtp = new SmtpClient(smtpAddress, portNumber);
                smtp.EnableSsl = enableSSL;
                smtp.Timeout = 100000;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(emailFrom, password);


                MailMessage mail = new MailMessage();
    
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                smtp.Send(mail);

            }
            catch(Exception ep)
            {
                exceptionLog.StoreExceptions(ep);
            }

        }
    }
}