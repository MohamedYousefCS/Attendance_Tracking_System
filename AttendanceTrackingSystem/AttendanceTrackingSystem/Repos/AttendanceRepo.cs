using AttendanceTrackingSystem.DBContext;
using AttendanceTrackingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace AttendanceTrackingSystem.Repos
{
    public interface IAttendance
    {
        public void ConfirmStudentAttendance(Attendance studentAttendance);

        public Attendance GetById(int id);

        Attendance GetByIdAndDate(int id, DateOnly date);

        public void ConfirmStudentCheckout(int Id);



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




    }
}
