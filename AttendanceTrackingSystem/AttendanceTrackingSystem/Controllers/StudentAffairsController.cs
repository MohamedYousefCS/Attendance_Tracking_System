using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

using OfficeOpenXml;

// Set the LicenseContext before using EPPlus
//ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // or LicenseContext.Commercial

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
           
            return View();
        }
        //======================read from excel sheet ===========================//
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

                     ImportDataFromExcel(filePath);

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

             return View("Index");
         }

       private void ImportDataFromExcel(string filePath)
         {
             using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
             {
                 ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                 int rowCount = worksheet.Dimension.End.Row;
                 int columnCount = worksheet.Dimension.Columns;

                 for (int row = 2; row <= rowCount; row++)
                 {
                     Student entity = new Student();

                     entity.Id = int.Parse(worksheet.Cells[row, 1].Value.ToString());
                     entity.Fname = worksheet.Cells[row, 2].Value.ToString();
                     entity.Lname = worksheet.Cells[row, 3].Value.ToString();
                     entity.Email = worksheet.Cells[row, 4].Value.ToString();
                     entity.Password = worksheet.Cells[row, 5].Value.ToString();
                    string genderStr = worksheet.Cells[row, 6].Value.ToString();
                    if (Enum.TryParse<Gender>(genderStr, out Gender gender))
                    {
                        entity.gender = gender;
                    }
                    entity.Mobile = worksheet.Cells[row, 7].Value.ToString();
                    string birthDateString = worksheet.Cells[row, 8].Value.ToString();

                    if (DateTime.TryParse(birthDateString, out DateTime birthDate))
                    {
                        entity.BirthDate = birthDate;
                    }
                    entity.Role = Role.Student;
                     entity.University = worksheet.Cells[row, 9].Value.ToString();
                     entity.Specialization = worksheet.Cells[row, 10].Value.ToString();
                     entity.GraduationYear =int.Parse( worksheet.Cells[row, 11].Value.ToString());
                     entity.trackId = int.Parse(worksheet.Cells[row, 12].Value.ToString());
                    trackrepo.students.Add(entity);
                 }

                 trackrepo.SaveChanges();
             }
         }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}

