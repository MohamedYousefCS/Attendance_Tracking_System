using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repos
{
    public interface IStudentRepo
    {

        public List<Student> GetAllStudents();
        public void AddStudent(Student student);
        public Student GetStudentById(int id);
        public void UpdateStudent(Student student);


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



    }
}
