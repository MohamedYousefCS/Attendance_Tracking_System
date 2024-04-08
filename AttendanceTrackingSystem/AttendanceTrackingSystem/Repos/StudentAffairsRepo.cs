using AttendanceTrackingSystem.DBContext;

namespace AttendanceTrackingSystem.Repos
{

    public interface IStudentAffairsRepo
    {

      // public IStudentAffairsRepo GetAllStudents();
       //public IStudentAffairsRepo GetById(int id);


    }
    public class StudentAffairsRepo: IStudentAffairsRepo
    {
        private ITIDBContext db;

        public StudentAffairsRepo(ITIDBContext _db )
        {
            db = _db;
        }


    }
}
