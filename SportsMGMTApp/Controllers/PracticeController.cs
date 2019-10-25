namespace SportsMGMTApp.Controllers
{
    using SportsMGMTApp.Filters;
    using SportsMGMTApp.Models;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using SportsMGMTCommon;
    using SportsMGMTBLL;
    using SportsMGMTApp.Mapper;
    using System;
    using SportsMGMTBLL.IOC;
    using Interfaces.IBusinessLogic;

    public class PracticeController : Controller
    {
        IAttendanceBLL attendance;
        IPractice practiceBLL;

        public PracticeController()
        {
            attendance = new AttendanceBLL(Resolve.Attendance());
            practiceBLL = new PracticeBLL(Resolve.Practice());
        }
        //create practice
        [HttpGet]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult AddPractice()
        {
            //returns the view to add a practice
            return View();
        }
        //submit practice
        [HttpPost]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult AddPractice(PracticeModel practice)
        {
            if (ModelState.IsValid)
            {
                var user = Session["Users"] as Users;
                //Map to common object
                PracticeMapper practiceMap = new PracticeMapper();
                Practice newPractice = practiceMap.PracticeMap(practice);
                newPractice.TeamID =user.TeamID;
         
                practiceBLL.CreatePractice(newPractice);
                List<Practice> getPractice = practiceBLL.GetPractice();

                if (getPractice.Exists(m => m.PracticeType == newPractice.PracticeType))
                {
                    ViewBag.Message = "Practice Added";
                }
                else
                {
                    ViewBag.Message = "Practice Failed";
                }
            }
            return View();
        }
        //list all practices that have been created with passing of ids to update practice and delete
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult ListPractice()
        {

            //store session variables for team id 
            var users = Session["Users"] as Users;
            //find all practices related to that coach or admins team
            List<Practice> listPractice = practiceBLL.GetPractice().FindAll(m => m.TeamID == users.TeamID);
            //return the list into the view
            return View(listPractice);
        }
        [HttpGet]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult UpdatePractice(int id)
        {
            //Make a new View Model
            PracticeModel practiceModel = new PracticeModel();

            //find the current practice object in database and store in object
            Practice practice = practiceBLL.GetPractice().Find(m => m.PracticeID == id);
            //set value to model property object practice
            practiceModel.practice = practice;
            //return view of the model to display object in database
            return View(practiceModel);
        }
        [HttpPost]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult UpdatePractice(PracticeModel model)
        {
            if (ModelState.IsValid)
            {

                //Find the current object in the database and store for check later
                Practice practice = practiceBLL.GetPractice().Find(m => m.PracticeID == model.practice.PracticeID);
                //perform update
                practiceBLL.UpdatePractice(model.practice);
                //Find the new updated practice in the database
                Practice practiceUpdate = practiceBLL.GetPractice().Find(m => m.PracticeID == model.practice.PracticeID);
                //if the two objects are not the same then the update was a success
                if (practice != practiceUpdate)
                {
                    //display Message
                    ViewBag.Message = "Update Successful";
                }
                else
                {
                    ViewBag.Message = "Update Failed";
                }
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
            }
            //return view
            model.PracticeType = model.practice.PracticeType;
            return View(model);
            
        }
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult DeletePractice (int id)
        {
            //Make a new View Model
            PracticeModel practiceModel = new PracticeModel();

            //find the current practice object in database and store in object
            Practice practice = practiceBLL.GetPractice().Find(m => m.PracticeID == id);
            //set value to model property object practice
            practiceModel.practice = practice;
            //return view of the model to display object in database
            return View(practiceModel);
        }
        [HttpPost]
        [MustBeInRole(Roles = "Admin,Coach")]
        public ActionResult DeletePractice (PracticeModel model)
        {

            //Find the current object in the database and store for check later
            Practice practice = practiceBLL.GetPractice().Find(m => m.PracticeID == model.practice.PracticeID);

            //Perform the delete in the database
            practiceBLL.DeletePractice(practice);

            //store in a boolean the list to check if the object still exists
            bool check = practiceBLL.GetPractice().Exists(m => m.PracticeID == model.practice.PracticeID);
            //if object doesn't exists in database list of objects
            if(check == false)
            {
                //display that the delete was successful
                ViewBag.Message = "Delete Successful";
            }
            else
            {
                ViewBag.Message = "Delete Failed";
            }
            //return view make sure to show in practice attendance view that cascading delete was successful
            return View(model);
        }
        /// <summary>
        /// For Coaches who want to tryout a potential member in free agency
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult TwoLevelPractice()
        {
            PracticeModel practice = new PracticeModel();
            var user = Session["Users"] as Users;
            practice.GetUsers(user.TeamID);
            return View(practice);
        }
        [HttpPost]
        [MustBeInRole(Roles="Admin,Coach")]
        public ActionResult TwoLevelPractice(PracticeModel practice)
        {
            if (ModelState.IsValid)
            {
                if (practice.PracticeType == "")
                {
                    ViewBag.Message = "Cannot leave practice blank";
                    return View(practice);
                }
                if(practice.StartTime < DateTime.Now)
                {
                    ViewBag.Message = "Start Time cannot be earlier than todays date";
                    return View(practice);
                }
                else if(practice.EndTime < DateTime.Now)
                {
                    ViewBag.Message = "End Time Cannot be  earlier than todays date";
                    return View(practice);
                }
                else if (practice.StartTime > practice.EndTime)
                {
                    ViewBag.Message = "Start Time Cannot be  later than End Time";
                    return View(practice);
                }
                else if(practice.StartTime==DateTime.MinValue || practice.EndTime == DateTime.MinValue)
                {
                    ViewBag.Message = "Cannot be the beggining of time";
                    return View(practice);
                }
                var users = Session["Users"] as Users;
                Practice createPractice = new Practice();
                PracticeAttended absent = new PracticeAttended();

                createPractice.PracticeType = practice.PracticeType;
                createPractice.StartTime = practice.StartTime;
                createPractice.EndTime = practice.EndTime;
                absent.UserID = practice.UserID;
                absent.Attended =practice.Check;
                createPractice.TeamID = users.TeamID;



                practiceBLL.CreatePractice(createPractice);
                List<Practice> check = practiceBLL.GetPractice();
                check.Reverse();
                Practice checkinsert = check.Find(m => m.PracticeType == createPractice.PracticeType && m.StartTime==createPractice.StartTime &&m.EndTime==createPractice.EndTime);
                absent.PracticeID = checkinsert.PracticeID;

                attendance.CreatePracticeAttendance(absent);

                bool insert = check.Exists(m => m.PracticeType == practice.PracticeType);
                bool checkInsert = attendance.getPracticeAttendaned(users.TeamID).Exists(m => m.PracticeID == check[0].PracticeID);

                if(insert && checkInsert)
                {
                    ViewBag.Message = "Practice and Attendance Created";

                }
                else if (insert && checkInsert == false)
                {
                    ViewBag.Message = "Only Practice Added";
                }
                else if(insert==false && checkInsert == false)
                {
                    ViewBag.Message = "No Insert Made";
                }
                else
                {

                }
                
            }
            else
            {
                ViewBag.Message = "Invalid Entry";
            }
            return View(practice);
        }


    }
}