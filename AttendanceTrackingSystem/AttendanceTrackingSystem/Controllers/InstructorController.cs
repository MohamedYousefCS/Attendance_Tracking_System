using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IInstructorRepo instRepo;

        public InstructorController(IUserRepo _userRepo, IInstructorRepo _instRepo)
        {
            instRepo = _instRepo;
            userRepo = _userRepo;
        }

        public IActionResult Index()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            User instructor = instRepo.GetInstructorByID(userId);
            return View(instructor);
        }
        public IActionResult Schedule()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            User instructor = instRepo.GetInstructorByID(userId);
            ViewBag.Role = "Supervisor";
            ViewBag.InstructorID = instructor.Id;
            ViewBag.TrackID = instRepo.GetTrackForInstructor(userId).TrackId;
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
