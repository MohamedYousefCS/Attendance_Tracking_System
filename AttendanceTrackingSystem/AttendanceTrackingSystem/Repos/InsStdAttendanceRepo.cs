using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repos
{
    public class InsStdAttendanceRepo
    {
         readonly ITIDBContext db;


        public InsStdAttendanceRepo(ITIDBContext _db)
        {
            db = _db;
        }
        public List<StudentWithAttendance> GetStudentsWithAttendanceForSupervisor(int supervisorId)
        {
            var studentsWithAttendance = db.students
                .Where(s => s.Track != null && s.Track.supervisorId == supervisorId) 
                .Select(s => new StudentWithAttendance
                {
                    Student = s,
                    Attendances = s.Attendances.ToList()
                })
                .ToList();


            return studentsWithAttendance;
        }

    }
}
