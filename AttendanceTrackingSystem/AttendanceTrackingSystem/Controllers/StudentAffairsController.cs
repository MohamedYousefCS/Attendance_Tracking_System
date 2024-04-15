using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace AttendanceTrackingSystem.Controllers
{
    public class StudentAffairsController : Controller
    {
         IStudentAffairsRepo StdAffairsRepo;
        
         IStudentRepo StudentRepo;
        ITIDBContext trackrepo = new ITIDBContext();

        public StudentAffairsController(IStudentAffairsRepo _repo, IStudentRepo _stuRepo)
        {
            StdAffairsRepo = _repo;
            StudentRepo= _stuRepo;
        }
        public IActionResult Index()
        {
            var students=StudentRepo.GetAllStudents();
            return View(students);
        }
        public IActionResult create()
        {
            ViewBag.tracks= trackrepo.tracks.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult create(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentRepo.AddStudent(student);
                return RedirectToAction("Index");
            }
            ViewBag.tracks = trackrepo.tracks.ToList();
            return View(student);
        }

        public IActionResult Edit( int? id)
        {
            if(id==null)
                return BadRequest();

            var Cruntstudent = StudentRepo.GetStudentById(id.Value);
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
                StudentRepo.UpdateStudent(student);
                return RedirectToAction("index");
            }
            ViewBag.tracks = trackrepo.tracks.ToList();
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
                return BadRequest();
            var student = StudentRepo.GetStudentById(id.Value);

            if (student == null)
                return NotFound(); // Student not found
                                   // Delete student and related records
            try
            {
                StudentRepo.DeleteStudent(id.Value);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                // Handle exception
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

                    StudentRepo.ImportDataFromExcel(filePath);

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

        public async Task<IActionResult> ViewAttendance(DateOnly date, Role? role = null)
        {
            AttendanceViewModel attendance;
            if (date == DateOnly.MinValue)
            {
                date = DateOnly.FromDateTime(DateTime.Now);
            }
            if (role == null)
            {
                attendance = await StdAffairsRepo.ViewAllAttendance(date);
                ViewBag.Role = null;
            }
            else
            {
                attendance = await StdAffairsRepo.ViewAttendance(role.Value, date);
                ViewBag.Role = (int)role.Value;
            }
            ViewBag.Date = date;
            return View(attendance);
        }




    }

}

