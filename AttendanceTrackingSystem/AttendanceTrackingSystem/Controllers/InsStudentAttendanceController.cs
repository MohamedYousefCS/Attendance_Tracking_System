using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class InsStudentAttendanceController : Controller
    {
         InsStdAttendanceRepo insStdAttendanceRepo;

        public InsStudentAttendanceController(InsStdAttendanceRepo _insstdattendanceRepo)
        {
            insStdAttendanceRepo = _insstdattendanceRepo;
        }
       public IActionResult GetStudentsWithAttendanceForSupervisor( int id)
        {
            var model = insStdAttendanceRepo.GetStudentsWithAttendanceForSupervisor(id);
            return View(model);
        }
    }
}
