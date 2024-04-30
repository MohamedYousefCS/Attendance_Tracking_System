using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System.Globalization;

namespace AttendanceTrackingSystem.Repos
{
    public interface IAttendance
    {
        public void ConfirmStudentAttendance(Attendance studentAttendance);
        public Attendance GetById(int id);
        Attendance GetByIdAndDate(int id, DateOnly date);
        public void ConfirmStudentCheckout(int Id);
        public List<Attendance> GetAllAttendance();
        public List<Student> GetAllAbsent();
        public void AutomateAttendance(List<Student> students);
        public Attendance GetAttendanceById(int id);
        public void Update(Attendance attendance);
    }

    public class AttendanceRepo:IAttendance
    {
        ITIDBContext db;
        public AttendanceRepo(ITIDBContext _db)
        {
            db = _db;
        }

        public void ConfirmStudentAttendance(Attendance studentAttendance)
        {
            db.attendances.Add(studentAttendance);
            db.SaveChanges();
        }

        public Attendance GetById(int id)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Today);
            return db.attendances.FirstOrDefault(a => a.userId == id && a.Date == currentDate && a.TimeOut == null);
        }

        public Attendance GetAttendanceById(int id)
        {
            return db.attendances.FirstOrDefault(a => a.AttendanceID == id);
        }

        public Attendance GetByIdAndDate(int id, DateOnly date)
        {
            return db.attendances.FirstOrDefault(a => a.userId == id && a.Date == date);
        }

        public void ConfirmStudentCheckout(int Id)
        {
            var studentAttendance = GetById(Id);
            if (studentAttendance != null)
            {
                studentAttendance.TimeOut = TimeOnly.FromDateTime(DateTime.Now);
                string time12HourFormat = studentAttendance.TimeOut.Value.ToString("hh:mm:ss tt");
                db.SaveChanges();
            }
        }

        public List<Attendance> GetAllAttendance()
        {
            return db.attendances.Include(a=>a.User).Where(a=> a.User.Role==Role.Student).ToList();
        }

        public List<Student> GetAllAbsent()
        {
            DateOnly curreentDate = DateOnly.FromDateTime(DateTime.Now);
            var users = db.users.Include(a => a.Attendances).Where(a => a.Role == Role.Student ).ToList();
            List<User> absentUsers = users.Where(u=>!u.Attendances.Any(a=>a.Date == curreentDate) || u.Attendances.Any(a=>a.Status==Status.Absent && a.Date == curreentDate)).ToList();
            List<Student> students = absentUsers.OfType<Student>().ToList();
            var studentsWithSchedule = students.Where(s => s.Track.Schedules.Any(sc => sc.Day == curreentDate)).ToList();
            return studentsWithSchedule;
        }

        public void AutomateAttendance(List<Student> students)
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            foreach (var student in students)
            {
                if (student.Attendances.Any(a => a.Date == currentDate))
                    continue;
                student.Attendances.Add(new Attendance { Date = currentDate, TimeIn = null, TimeOut = null, Status = Status.Absent });
            }
            db.SaveChanges();
        }

        public void Update(Attendance attendance)
        {
            db.Update(attendance);
            db.SaveChanges();
        }
    }
}
