namespace AttendanceTrackingSystem.Models
{
    public class StudentAffairs:Employee
    {
        public Role role { get; private set; } = Role.StudentAffairs;

    }
}
