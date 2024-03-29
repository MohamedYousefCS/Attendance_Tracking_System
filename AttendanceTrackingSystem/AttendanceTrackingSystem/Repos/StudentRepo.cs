using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.Repos
{
    public interface IStudentRepo
    {

        public List<Student> GetAllStudents();

    }


    public class StudentRepo:IStudentRepo
    {
        ITIDBContext db;

        public StudentRepo(ITIDBContext db)
        {
            this.db = db;
        }

        public List<Student> GetAllStudents()
        {
            return db.students.ToList();
        }


    }
}
