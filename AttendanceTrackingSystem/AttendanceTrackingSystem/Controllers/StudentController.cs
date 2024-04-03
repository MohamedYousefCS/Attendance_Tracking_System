using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class StudentController : Controller
    {


        IStudentRepo stuRepo;

        public StudentController(IStudentRepo _stuRepo)
        {
            stuRepo = _stuRepo;
        }


        public IActionResult Index()
        {
            //var model = stuRepo.GetAllStudents();
            return View();
        }



    }
}
