using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AttendanceTrackingSystem.Controllers
{
    public class StudentController : Controller
    {


        IStudentRepo stuRepo;
        IUserRepo userRepo;

        public StudentController(IStudentRepo _stuRepo,  IUserRepo user)
        {
            stuRepo = _stuRepo;
            userRepo = user;
        }

        public IActionResult Index(int  id)
        {
            var model=stuRepo.GetStudentById(id);
            if (model == null)
                return NotFound();

            return View(model);
        }
        public IActionResult showAttendance(int id)
        {
            var model = stuRepo.GetStudentByIdWithAttendacne(id);
            if (model == null)
                return NotFound();
            
            Dictionary<string, int> lateAttendanceSummary = stuRepo.GetLateAttendanceSummary(id);
            Dictionary<string, int> AbsentAttendanceSummary = stuRepo.GetAbsentAttendanceSummary(id);

            // late
            int lateDaysWithPermission = lateAttendanceSummary["LateDaysWithPermission"];
            int lateDaysWithoutPermission = lateAttendanceSummary["LateDaysWithoutPermission"];

            //Absent 
            int AbsentDaysWithPermission = AbsentAttendanceSummary["AbsentDaysWithPermission"];
            int AbsentDaysWithoutPermission = AbsentAttendanceSummary["AbsentDaysWithoutPermission"];

            

            ViewBag.tatalDegree= stuRepo.GetStudetnDegree(id);
            ViewBag.absentDays = AbsentDaysWithPermission + AbsentDaysWithoutPermission;
            ViewBag.lateDays = lateDaysWithPermission + lateDaysWithoutPermission;
            return View(model);
        }

        public ActionResult AttendanceData(DateOnly date, DateOnly date2)
        {
            var attendances = stuRepo.GetAllAttendance(date, date2);
            return PartialView("_AttendanceTable", attendances);
        }
        public IActionResult permission(int id)
        {
            var permissions = stuRepo.GetAllPermissionRequest(id);

            return View(permissions);
        }
        public IActionResult DeletePermission(int id)
        {
            
            if(id ==0)  
                return NotFound();  

            stuRepo.DeletePermission(id);
            var permissions = stuRepo.GetAllPermissionRequest(3);

            return View("permission",permissions);
        }

    
        public IActionResult createPermission()
        {
            return View();
        }
        [HttpPost]
        public IActionResult createPermission(PermissionRequest PR)
        {
            if (ModelState.IsValid)
            {
                int userId = int.Parse(User.FindFirstValue("UserId"));
                PermissionRequest permissionRequest = new PermissionRequest
                {
                    IsAccepted = IsAccepted.pending,
                    Reason = PR.Reason,
                    Date = PR.Date,
                    Type = PR.Type,
                    studentId = userId,
                };

                stuRepo.AddPermission(permissionRequest);
                var permissions = stuRepo.GetAllPermissionRequest(userId);
                ViewBag.Model = permissions;
                return RedirectToAction("permission");
            }
            return View(PR);
        }

        public IActionResult GetList()
        {
            var model = new List<object>
            {
                new {Name = "Eslam", Faculty = "engineering", University = "kokok", Email="Mohamef@gmail.com" },
                new {Name = "Em", Faculty = "enging", University = "klllok", Email="Moha@gmail.com" }
            };
            var records = stuRepo.GeStudentAttendances(8);

            return Json(new { data = model });
        }
    }
}
