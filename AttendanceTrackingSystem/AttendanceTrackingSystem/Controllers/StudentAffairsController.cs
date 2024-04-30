using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;


namespace AttendanceTrackingSystem.Controllers
{
    public class StudentAffairsController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly IStudentAffairsRepo stdAffairsRepo;
        private readonly IStudentRepo studentRepo;
        private readonly IEmployeeRepo empRepo;
        private readonly IAttendance attendance;
        private readonly ITIDBContext trackrepo = new ITIDBContext();

        public StudentAffairsController(IUserRepo _userRepo, IStudentAffairsRepo _repo, IStudentRepo _stuRepo, IEmployeeRepo _empRepo,IAttendance _attendance)
        {
            userRepo = _userRepo;
            stdAffairsRepo = _repo;
            studentRepo = _stuRepo;
            empRepo = _empRepo;
            attendance = _attendance;
        }

        public IActionResult Index()
        {
            int userId = userRepo.GetCurrentUserId(HttpContext.User);
            Employee StuAff = stdAffairsRepo.GetStuAffById(userId);
            return View(StuAff);
        }

        public IActionResult AllStudents()
        {
			var students = studentRepo.GetAllStudents();
			return View(students);
		}

        public IActionResult Create()
        {
            ViewBag.tracks= trackrepo.tracks.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                studentRepo.AddStudent(student);
                return RedirectToAction("Index");
            }
            ViewBag.tracks = trackrepo.tracks.ToList();
            return View(student);
        }

        public IActionResult Edit( int? id)
        {
            if(id==null)
                return BadRequest();
            var Cruntstudent = studentRepo.GetStudentById(id.Value);
            if( Cruntstudent==null )
                return NotFound();
            ViewBag.tracks = trackrepo.tracks.ToList();
            return View(Cruntstudent);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Student student)
        {
            if (id == null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                studentRepo.UpdateStudent(student);
                return RedirectToAction("index");
            }
            ViewBag.tracks = trackrepo.tracks.ToList();
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
                return BadRequest();
            var student = studentRepo.GetStudentById(id.Value);
            if (student == null)
                return NotFound();
            try
            {
                studentRepo.DeleteStudent(id.Value);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult UploadExcel(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", fileName);
                    using (FileStream stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    studentRepo.ImportDataFromExcel(filePath);
                    ViewBag.Message = "Bulk insert from Excel to database successful!";
                }
                else
                {
                    ViewBag.Message = "No file uploaded.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error: " + ex.Message;
            }
            return RedirectToAction("index");
        }

        [HttpGet("StudentAffairs/TakeAttendance")]
        public IActionResult TakeEmployeeAttendance()
        {
            var model = empRepo.GetAllEmployees();
            var propertyNames = new List<string> { "Id","Fname", "Lname","Role" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "StudentAffairs";
            ViewBag.Action = "TakeAttendance";
            ViewBag.Attendance = attendance;
            return View(model);
        }

        [HttpGet("StudentAffairs/AllAttendance")]
        public IActionResult AllAttendance()
        {
            var model = attendance.GetAllAttendance();
            var propertyNames = new List<string> { "AttendanceID", "Fname", "Lname", "Date", "TimeIn", "TimeOut", "Status","Role" };
            ViewBag.PropertiesToShow = propertyNames;
            ViewBag.Controller = "StudentAffairs";
            ViewBag.Action = "AllAttendance";
            return View(model);
        }

        public IActionResult UpdateAttendance(int id)
        {
            ViewBag.WideView = "Wide";
            var model = attendance.GetAttendanceById(id);
            if (model == null)
                return NotFound();
            return View(model);
        }


        [HttpPost]
        public IActionResult UpdateAttendance(Attendance attend, int id)
        {

            if (ModelState.IsValid)
            {
                attendance.Update(attend);
                return RedirectToAction("AllAttendance");
            }
            else
            {
                return View("UpdateAttendance", attend);
            }
        }

        public async Task<IActionResult> ViewAttendance(DateOnly date, Role? role = null)
        {
            AttendanceViewModel attendance;
            if (date == DateOnly.MinValue)
            {
                date = DateOnly.FromDateTime(DateTime.Now);
            }
            if (role == null)
            {
                attendance = await stdAffairsRepo.ViewAllAttendance(date);
                ViewBag.Role = null;
            }
            else
            {
                attendance = await stdAffairsRepo.ViewAttendance(role.Value, date);
                ViewBag.Role = (int)role.Value;
            }
            ViewBag.Date = date;
            return View(attendance);
        }
        
        public async Task<IActionResult> AutomateAbsentRecords()
        {
            var model = attendance.GetAllAbsent();
            attendance.AutomateAttendance(model);
            return RedirectToAction("AllAttendance");
        }

        public async Task<IActionResult> CaclcDegree(int id)
        {
            var degree = studentRepo.GetStudetnDegree(id);
            return Json(new { degree}); ;
        }
    }

}

