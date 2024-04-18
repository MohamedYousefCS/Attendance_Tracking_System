using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var model=stuRepo.GetStudentById(2);
            if (model == null)
                return NotFound();

            return View(model);
        }
        public IActionResult showAttendance()
        {
            var model = stuRepo.GetStudentByIdWithAttendacne(2);
            if (model == null)
                return NotFound();
            
                        Dictionary<string, int> lateAttendanceSummary = stuRepo.GetLateAttendanceSummary(2);
                        Dictionary<string, int> AbsentAttendanceSummary = stuRepo.GetAbsentAttendanceSummary(2);

                        // late
                        int lateDaysWithPermission = lateAttendanceSummary["LateDaysWithPermission"];
                        int lateDaysWithoutPermission = lateAttendanceSummary["LateDaysWithoutPermission"];

                        //Absent 
                        int AbsentDaysWithPermission = AbsentAttendanceSummary["AbsentDaysWithPermission"];
                        int AbsentDaysWithoutPermission = AbsentAttendanceSummary["AbsentDaysWithoutPermission"];

                        // Now you can use these counts as needed
                       /* Console.WriteLine("Late days with permission: " + lateDaysWithPermission);
                        Console.WriteLine("Late days without permission: " + lateDaysWithoutPermission);      
                        Console.WriteLine("Absent days with permission: " + AbsentDaysWithPermission);
                        Console.WriteLine("Absent days without permission: " + AbsentDaysWithoutPermission);
                       */
            

            ViewBag.tatalDegree= stuRepo.calcTotalDegree(2);
            ViewBag.absentDays = AbsentDaysWithPermission + AbsentDaysWithoutPermission;
            ViewBag.lateDays = lateDaysWithPermission + lateDaysWithoutPermission;
            return View(model);
        }

        public ActionResult AttendanceData(DateOnly date, DateOnly date2)
        {
            var attendances = stuRepo.GetAllAttendance(date, date2);
            return PartialView("_AttendanceTable", attendances);
        }
        public IActionResult permission()
        {
            var permissions = stuRepo.GetAllPermissionRequest(2);

            return View(permissions);
        }
        public IActionResult DeletePermission(int id)
        {
            
            if(id ==0)  
                return NotFound();  

            stuRepo.DeletePermission(id);
            var permissions = stuRepo.GetAllPermissionRequest(2);

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
                PermissionRequest permissionRequest = new PermissionRequest
                {
                    IsAccepted = IsAccepted.pending,
                    Reason = PR.Reason,
                    Date = PR.Date,
                    Type = PR.Type,
                    studentId = 2,
                };

                stuRepo.AddPermission(permissionRequest);
                var permissions = stuRepo.GetAllPermissionRequest(2);
                return RedirectToAction("permission", permissions);
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
