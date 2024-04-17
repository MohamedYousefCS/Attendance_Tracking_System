using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class InstructorController : Controller
    {
        private IInstructorRepo instRepo;

        public InstructorController(IInstructorRepo _instRepo)
        {
            instRepo = _instRepo;

        }

        public IActionResult Index()
        {
            User instructor = instRepo.GetInstructorByID(1);
            return View(instructor);
        }
        public IActionResult Schedule()
        {
            // get the user id from the session add it later ok
            //User instructor = instRepo.GetInstructorByID(1);

            ViewBag.Role = "Supervisor";
            ViewBag.InstructorID = 2;//add it later
            ViewBag.TrackID = 1;//add it later
            return View();
        }
        public IActionResult AddSchedule(Schedule s)
        {
            
            instRepo.AddSchedule(s);
            var success = true;
            return Json(new { success });
        }

    }
}
