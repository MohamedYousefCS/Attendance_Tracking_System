using AttendanceTrackingSystem.Models;

namespace AttendanceTrackingSystem.ViewModels
{
    public class AttendanceViewModel
    {
        public List<User> Present { get; set; }
        public List<User> Absent { get; set; }
    }
}
