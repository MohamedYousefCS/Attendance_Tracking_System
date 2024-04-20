using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using AttendanceTrackingSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AttendanceTrackingSystem.Repos
{

    public interface IStudentAffairsRepo
    {
        public Employee GetStuAffById(int id);

        // public IStudentAffairsRepo GetAllStudents();
        //public IStudentAffairsRepo GetById(int id);'
        public Task<AttendanceViewModel> ViewAllAttendance(DateOnly date);
        public Task<AttendanceViewModel> ViewAttendance(Role role, DateOnly date);




    }
    public class StudentAffairsRepo: IStudentAffairsRepo
    {
        private ITIDBContext db;

        public StudentAffairsRepo(ITIDBContext _db )
        {
            db = _db;
        }
        public Employee GetStuAffById(int id)
        {
            return db.employees.FirstOrDefault(emp => emp.Id == id);
        }
        public async Task<AttendanceViewModel> ViewAllAttendance(DateOnly date)
        {

            AttendanceViewModel attendance = new AttendanceViewModel();
            var users = await db.users.Include(u => u.Attendances).ToListAsync();
            attendance.Present = users.Where(u => u.Attendances.Any(a => a.Date == date && a.Status != Status.Absent)).ToList();
            attendance.Absent = users.Where(u => !u.Attendances.Any(a => a.Date == date) || u.Attendances.Any(a => a.Date == date && a.Status == Status.Absent)).ToList();
            return attendance;
        }
        public async Task<AttendanceViewModel> ViewAttendance(Role role, DateOnly date)
        {
            AttendanceViewModel attendance = new AttendanceViewModel();
            var users = await db.users.Include(u => u.Attendances).Where(u => u.Role == role).ToListAsync();
            attendance.Present = users.Where(u => u.Attendances.Any(a => a.Date == date && a.Status != Status.Absent)).ToList();
            attendance.Absent = users.Where(u => !u.Attendances.Any(a => a.Date == date) || u.Attendances.Any(a => a.Date == date && a.Status == Status.Absent)).ToList();
            return attendance;
        }




    }
}
