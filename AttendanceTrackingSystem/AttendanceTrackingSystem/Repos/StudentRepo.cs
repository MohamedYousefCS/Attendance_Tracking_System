﻿using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Linq;

namespace AttendanceTrackingSystem.Repos
{
    public interface IStudentRepo
    {
        public List<Student> GetAllStudents();
        public void AddStudent(Student student);
        public Student GetStudentById(int id);
        public void UpdateStudent(Student student);
        public void DeleteStudent(int id);
        public void ImportDataFromExcel(string filePath);
        public Task AddAttendanceRecord();

        public int GetStudetnDegree(int id);
        public List<Attendance> GeStudentAttendances(int id);
    }


    public class StudentRepo:IStudentRepo
    {
        ITIDBContext db;

        public StudentRepo(ITIDBContext _db)
        {
            db = _db;
        }

        public List<Student> GetAllStudents()
        {
            return db.students.Include(a=>a.Track).ToList();
        }
        public void AddStudent(Student student)
        {
            db.students.Add(student);
            db.SaveChanges();
        }
        public Student GetStudentById(int id)
        {
        return db.students.FirstOrDefault(a=>a.Id == id);  
        }
        public void UpdateStudent(Student student)
        {
            db.students.Update(student);
            db.SaveChanges();
        }
        public void DeleteStudent(int id)
        {
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    // Delete attendance records
                    var stdAttendaces = db.attendances.Where(a => a.userId == id).ToList();
                    if (stdAttendaces.Any())
                    {
                        db.attendances.RemoveRange(stdAttendaces);
                    }

                    // Delete permission requests
                    var stdPermissiones = db.permissionRequests.Where(p => p.studentId == id).ToList();
                    if (stdPermissiones.Any())
                    {
                        db.permissionRequests.RemoveRange(stdPermissiones);
                    }

                    // Delete student
                    var studentToDelete = db.students.Find(id);
                    if (studentToDelete != null)
                    {
                        db.students.Remove(studentToDelete);
                    }

                    // Save changes
                    db.SaveChanges();

                    // Commit transaction
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Rollback transaction in case of exception
                    transaction.Rollback();
                    throw; // Rethrow the exception to be handled in the caller
                }
            }
        }



        public void ImportDataFromExcel(string filePath)
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                int rowCount = worksheet.Dimension.End.Row;
                int columnCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++)
                {
                    Student entity = new Student();
                    entity.Fname = worksheet.Cells[row, 1].Value.ToString();
                    entity.Lname = worksheet.Cells[row, 2].Value.ToString();
                    entity.Email = worksheet.Cells[row, 3].Value.ToString();
                    entity.Password = worksheet.Cells[row, 4].Value.ToString();
                    string genderStr = worksheet.Cells[row, 5].Value.ToString();
                    if (Enum.TryParse<Gender>(genderStr, out Gender gender))
                    {
                        entity.gender = gender;
                    }
                    entity.Mobile = worksheet.Cells[row, 6].Value.ToString();
                    string birthDateString = worksheet.Cells[row, 7].Value.ToString();

                    if (DateTime.TryParse(birthDateString, out DateTime birthDate))
                    {
                        entity.BirthDate = birthDate;
                    }
                    entity.Role = Role.Student;
                    entity.University = worksheet.Cells[row, 8].Value.ToString();
                    entity.Faculty = worksheet.Cells[row, 9].Value.ToString();
                    entity.Specialization = worksheet.Cells[row, 10].Value.ToString();
                    entity.GraduationYear = int.Parse(worksheet.Cells[row, 11].Value.ToString());
                    entity.trackId = int.Parse(worksheet.Cells[row, 12].Value.ToString());
                    db.students.Add(entity);
                }

                db.SaveChanges();

            }
            

        }
        
        public int GetStudetnDegree(int id)
        {
            int counter = 0;
            int counter2= 0;
            int degere = 250;

            List<Attendance> stdRecordWithPermission = db.attendances.Where(a => a.userId == id && a.Status == Status.Absent&& a.PermissionType == PermissionType.Excused).ToList();
            List<Attendance> stdRecordWithoutPerm = db.attendances.Where(a => a.userId == id && a.Status == Status.Absent && a.IsPermission == false).ToList();

            var daysWithPermission = stdRecordWithPermission.Count;
            var daysWithoutPermission = stdRecordWithoutPerm.Count;
            while (daysWithPermission > 0)
            {
                if(counter == 0)
                {
                    counter++;
                    daysWithPermission--;
                    continue;
                }
                else if (counter <=3 )
                {
                    counter++;
                    daysWithPermission--;
                    degere -= 5;                    
                }
                else if(counter <= 6 && counter > 3)
                {
                    counter++;
                    daysWithPermission--;
                    degere -= 10;
                }
                else
                {
                    daysWithPermission--;
                    degere -= 15;
                }
            }
            while (daysWithoutPermission > 0)
            {
                if(counter2 == 0 && degere == 250)
                {
                    counter2++;
                    daysWithoutPermission--;
                }
                else
                {
                    daysWithoutPermission--;
                    degere -= 25;
                }
              
            }
            return degere;
        }
        public async Task AddAttendanceRecord()
        {
           
                // Create a new Attendance object and set its properties
                var newAttendance = new Attendance
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    TimeIn = TimeOnly.FromDateTime(DateTime.Now),
                    // Assuming TimeOut is not mandatory and can be null
                    TimeOut = null,
                    Status = Status.Absent,  
                    IsPermission = false,  
                    PermissionType = Models.PermissionType.None,
                    userId = 8  
                };
                db.attendances.Add(newAttendance);

                await db.SaveChangesAsync();
            }

        public List<Attendance> GeStudentAttendances(int id)
        {
            List<Attendance> userAttendance = db.users.Include(u => u.Attendances).FirstOrDefault(a => a.Id == id).Attendances;
            return userAttendance;
        }

    }
}
