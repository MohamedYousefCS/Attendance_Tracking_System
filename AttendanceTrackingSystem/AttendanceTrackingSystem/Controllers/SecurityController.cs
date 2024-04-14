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
        ITIDBContext db;


        public SecurityController(IStudentRepo stuRepo, IAttendance attendance, AdTrackRepo trackrepo)
        {
            this.stuRepo = stuRepo;
            Attendance = attendance;
            this.trackrepo = trackrepo;
        }
        public IActionResult Index()
        {

            return View();
        }

        public ActionResult GetAllTracks()
        {

            var tracks = trackrepo.GetAllTracks();
            return View(tracks);
        }

        //[HttpGet("GetStudentByTrackID/{id}")]
        public IActionResult GetStudentByTrackID([FromRoute] int id)
        {
            var students = trackrepo.GetStudentsByTrackId(id);
            return View(students);
        }

        [HttpPost]
        public IActionResult ConfirmStudentAttendace([FromRoute] int Id)
        {
            DateTime studentDate = DateTime.Now;
            DateTime dateOnly = studentDate.Date;
            string studentTime = studentDate.ToString("hh:mm:ss");
            string correctTime = String.Format("09:00:00");
            Attendance studentAttendance = new Attendance() { Date = DateOnly.Parse(dateOnly.ToString("yyyy-MM-dd")), TimeIn = TimeOnly.Parse(studentTime), userId = Id };


            TimeSpan studentTimeSpan = TimeSpan.Parse(studentTime);
            TimeSpan correctTimeSpan = TimeSpan.Parse(correctTime);

            int comparison = TimeSpan.Compare(studentTimeSpan, correctTimeSpan);

            if (comparison == 0)
            {

                studentAttendance.Status = Status.Present;
            }
            else if (comparison > 0)
            {
                studentAttendance.Status = Status.Late;
            }
            else
            {
                studentAttendance.Status = Status.Present;
            }
            Attendance.ConfirmStudentAttendance(studentAttendance);

            return RedirectToAction("GetAllTracks");


        }



        public IActionResult ConfirmStudentCheckout([FromRoute] int Id)
        {

            Attendance.ConfirmStudentCheckout(Id);

            return RedirectToAction("GetAllTracks");

        }

    }

        
}
