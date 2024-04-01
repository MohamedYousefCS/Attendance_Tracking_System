namespace AttendanceTrackingSystem.Models
{
    public class Security:Employee
    {
        public Role role { get; private set; }=Role.Security;
    }
}
