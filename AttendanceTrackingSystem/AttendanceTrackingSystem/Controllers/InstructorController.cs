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
    }
}
