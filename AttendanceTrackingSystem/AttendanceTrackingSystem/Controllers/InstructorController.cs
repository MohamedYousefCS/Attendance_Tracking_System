using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            int userId = int.Parse(User.FindFirstValue("UserId"));

            User instructor = instRepo.GetInstructorByID(userId);
            return View(instructor);
        }
        public IActionResult Schedule()
        {
            // get the user id from the session add it later ok
            int userId = int.Parse(User.FindFirstValue("UserId"));

            User instructor = instRepo.GetInstructorByID(userId);

            ViewBag.Role = "Supervisor";
            ViewBag.InstructorID = instructor.Id;
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
