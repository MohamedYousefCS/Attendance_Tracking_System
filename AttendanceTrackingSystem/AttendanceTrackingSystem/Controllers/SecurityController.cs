using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IStudentRepo stuRepo;
        private readonly IAttendance Attendance;
        private readonly AdTrackRepo trackrepo;
        private readonly IInstructorRepo instructorRepo;
        private readonly IEmployeeRepo emplyeeRepo;

        public SecurityController(IUserRepo _userRepo, IStudentRepo _stuRepo, IAttendance _attendance, AdTrackRepo _trackrepo, IInstructorRepo _instructorRepo, IEmployeeRepo _emplyeeRepo)
        {
            userRepo = _userRepo;
            stuRepo = _stuRepo;
            Attendance = _attendance;
            trackrepo = _trackrepo;
            instructorRepo = _instructorRepo;
            emplyeeRepo = _emplyeeRepo;
        }

        public IActionResult Index()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var model = emplyeeRepo.GetById(userId);
            return View(model);
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
            ViewBag.Attendance = Attendance;
            return View(students);
        }
        [HttpGet("Security/AllInstructors")]

        public IActionResult AllInstructors()
        {
            var instructors=instructorRepo.GetAllInstructor();
            var propertyNames = new List<string> { "Id", "Fname", "Lname","Role" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "Security";
            ViewBag.Action = "AllInstructors";
            ViewBag.Attendance = Attendance;
            return View(instructors);
        }


        public IActionResult CheckStudentAttendace(int Id)
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Today);
            Attendance existingAttendance = Attendance.GetByIdAndDate(Id, todayDate);
            int isAdded;
            if (existingAttendance == null)
            {
                isAdded = 1;
                return Json(new { isAdded = isAdded });
            }
            if (existingAttendance.TimeIn != null && existingAttendance.TimeOut == null)
            {
                isAdded = 0;
                return Json(new { isAdded = isAdded });
            }
            if(existingAttendance.TimeOut == null)
            {
                isAdded = 1;
                return Json(new { isAdded = isAdded });
            }
            else { 
                isAdded = 2;
                return Json(new { isAdded = isAdded });
            }
        }


        public IActionResult ConfirmStudentAttendace(int Id)
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.Today);
            Attendance existingAttendance = Attendance.GetByIdAndDate(Id, todayDate);
            bool isAdded;
            if (existingAttendance != null)
            {
                isAdded = true;
                return Json(new { isAdded = isAdded });
            }

            DateTime.Now.Date.ToString("HH:mm:ss");
            DateTime studentDate = DateTime.Now;
            DateTime dateOnly = studentDate.Date;
            string studentTime = studentDate.ToString("HH:mm:ss");
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
