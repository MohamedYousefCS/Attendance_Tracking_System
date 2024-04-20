using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingSystem.Controllers
{
    public class EmployeeController : Controller
    {

        IEmployeeRepo EmpRepo;

        public EmployeeController(IEmployeeRepo _EmpRepo)
        {
            this.EmpRepo = _EmpRepo;
        }
        public IActionResult Index()
        {
            ViewBag.WideView = "Wide";
            var model = EmpRepo.GetAllEmployees();
            var propertyNames = new List<string> { "Name", "Email", "Salary", "Mobile", "Role" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "Employee";
            ViewBag.Action = "index";
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.WideView = "Wide";
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee Emp)
        {
            if (!ModelState.IsValid)
                return View("Create", Emp);
            EmpRepo.Add(Emp);
            return RedirectToAction("Index");

        }

        public IActionResult Details(int? id)
        {
            ViewBag.WideView = "Wide";
            if (id == null)
                return BadRequest();
            var model = EmpRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }

        public IActionResult Update(int? id)
        {
            ViewBag.WideView = "Wide";
            if (id == null)
                return BadRequest();
            var model = EmpRepo.GetById(id.Value);
            if (model == null)
                return NotFound();
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(Employee Emp, int id)
        {

            if (ModelState.IsValid)
            {
                EmpRepo.Update(Emp);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Update", Emp);

            }
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var employee = EmpRepo.GetById(id.Value);
            if (employee == null)
                return NotFound();

            EmpRepo.Delete(id.Value);

            return RedirectToAction("Index");

        }
    }
}
