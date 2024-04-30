using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AttendanceTrackingSystem.Controllers
{
    public class InsStudentAttendanceController : Controller
    {
         private readonly InsStdAttendanceRepo insStdAttendanceRepo;
         private readonly IUserRepo userRepo;

        public InsStudentAttendanceController(IUserRepo _userRepo, InsStdAttendanceRepo _insstdattendanceRepo)
        {
            userRepo = _userRepo;
            insStdAttendanceRepo = _insstdattendanceRepo;
        }
       public IActionResult GetStudentsWithAttendanceForSupervisor()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var model = insStdAttendanceRepo.GetStudentsWithAttendanceForSupervisor(userId);
            return View(model);
        }
    }
}
