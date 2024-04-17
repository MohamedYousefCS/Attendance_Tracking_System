using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Controllers
{
    public class SecurityController : Controller
    {
        IStudentRepo stuRepo;
        IAttendance Attendance;
        AdTrackRepo trackrepo;
        IInstructorRepo instructorRepo;


        public SecurityController(IStudentRepo stuRepo, IAttendance attendance, AdTrackRepo trackrepo, IInstructorRepo instructorRepo)
        {
            this.stuRepo = stuRepo;
            Attendance = attendance;
            this.trackrepo = trackrepo;
            this.instructorRepo = instructorRepo;
        }
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet("Security/Tracks")]

        public ActionResult GetAllTracks()
        {

            var tracks = trackrepo.GetAllTracks();
            return View(tracks);
        }

        [HttpGet("Security/AllStudents/{id}")]
        public IActionResult GetStudentByTrackID([FromRoute] int id)
        {
            var students = trackrepo.GetStudentsByTrackId(id);
            var propertyNames = new List<string> { "Id","Fname", "Lname", "TrackName" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "Security";
            ViewBag.Action = "AllStudents";
            return View(students);
        }
        [HttpGet("Security/AllInstructors")]

        public IActionResult AllInstructors()
        {
            var instructors=instructorRepo.GetInstructorList();
            var propertyNames = new List<string> { "Id", "Fname", "Lname" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "Security";
            ViewBag.Action = "AllInstructors";
            return View(instructors);
        }





        public IActionResult ConfirmStudentAttendace(int Id)
        {
            // Get today's date
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Today);

            // Check if the record already exists for the given userId and date
            Attendance existingAttendance = Attendance.GetByIdAndDate(Id, todayDate);

            var isAdded=false;

            if (existingAttendance != null)
            {
                // If the record already exists, return an error or handle as needed
                isAdded = true; // Indicate that the record was not added
                return Json(new { isAdded = isAdded });
            }

            DateTime.Now.Date.ToString("hh:mm:ss");
            DateTime studentDate = DateTime.Now;
            DateTime dateOnly = studentDate.Date;
            string studentTime = studentDate.ToString("hh:mm:ss");
            string correctTime = String.Format("09:00:00");
            Attendance studentAttendance = new Attendance() { Date = DateOnly.Parse(dateOnly.ToString("yyyy-MM-dd")), TimeIn = TimeOnly.Parse(studentTime), userId = Id };


            TimeSpan studentTimeSpan = TimeSpan.Parse(studentTime);
            TimeSpan correctTimeSpan = TimeSpan.Parse(correctTime);

            int comparison = TimeSpan.Compare(studentTimeSpan, correctTimeSpan);

            if (comparison <= 0)
            {
                studentAttendance.Status = Status.Present;
            }
            else
            {
                studentAttendance.Status = Status.Late;
            }
            
            Attendance.ConfirmStudentAttendance(studentAttendance);

             isAdded = true;
            return Json(new { isAdded = isAdded });
        }



        public IActionResult ConfirmStudentCheckout( int Id)
        {

            Attendance.ConfirmStudentCheckout(Id);
            var isAdded = true;
            return Json(new { isAdded});

        }


    }
    }
