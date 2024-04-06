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
            var model = stuRepo.GetAllStudents();          
            var propertyNames = new List<string> { "Name", "Email", "University", "Faculty" };
            ViewBag.PropertiesToShow = propertyNames;
            return View(model);
        }

        public IActionResult GetList()
        {
            var model = new List<object>
            {
                new {Name = "Eslam", Faculty = "engineering", University = "kokok", Email="Mohamef@gmail.com" },
                new {Name = "Em", Faculty = "enging", University = "klllok", Email="Moha@gmail.com" }
            };
            //var model = stuRepo.GetAllStudents();
            return Json(new { data = model });
        }

    }
}
