namespace AttendanceTrackingSystem.Models
{
    public class StudentWithAttendance
    {
        public Student Student { get; set; }
        public List<Attendance> Attendances { get; set; } = new List<Attendance>();
    }
}
