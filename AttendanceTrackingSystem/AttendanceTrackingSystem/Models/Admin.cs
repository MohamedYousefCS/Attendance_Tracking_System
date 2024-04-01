namespace AttendanceTrackingSystem.Models
{
    public class Admin:User
    {
        public Role role { get; private set; } = Role.Admin;
    }
}
