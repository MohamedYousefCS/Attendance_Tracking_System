using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AttendanceTrackingSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IStudentRepo stuRepo;

        public StudentController(IUserRepo _userRepo, IStudentRepo _stuRepo)
        {
            userRepo = _userRepo;
            stuRepo = _stuRepo;
        }

        public IActionResult Index()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var model =stuRepo.GetStudentById(userId);
            if (model == null)
                return NotFound();

            return View(model);
        }

        public IActionResult ShowAttendance()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var model = stuRepo.GetStudentByIdWithAttendacne(userId);
            if (model == null)
                return NotFound();
            Dictionary<string, int> lateAttendanceSummary = stuRepo.GetLateAttendanceSummary(userId);
            Dictionary<string, int> AbsentAttendanceSummary = stuRepo.GetAbsentAttendanceSummary(userId);
            // late
            int lateDaysWithPermission = lateAttendanceSummary["LateDaysWithPermission"];
            int lateDaysWithoutPermission = lateAttendanceSummary["LateDaysWithoutPermission"];
            //Absent 
            int AbsentDaysWithPermission = AbsentAttendanceSummary["AbsentDaysWithPermission"];
            int AbsentDaysWithoutPermission = AbsentAttendanceSummary["AbsentDaysWithoutPermission"];
            ViewBag.tatalDegree= stuRepo.GetStudetnDegree(userId);
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
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var permissions = stuRepo.GetAllPermissionRequest(userId);
            return View(permissions);
        }

        public IActionResult DeletePermission(int id)
        {
            if(id ==0)  
                return NotFound();  
            stuRepo.DeletePermission(id);
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            var permissions = stuRepo.GetAllPermissionRequest(userId);
            return RedirectToAction("permission", permissions);
        }

        public IActionResult createPermission()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            ViewBag.student= stuRepo.GetStudentById(userId);
            return View();
        }

        [HttpPost]
        public IActionResult createPermission(PermissionRequest PR)
        {
            if (ModelState.IsValid)
            {
                int userId = userRepo.GetCurrentUserId(HttpContext.User);
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
                return View("permission", permissions);
            }
            return View(PR);
        }
    }
}
