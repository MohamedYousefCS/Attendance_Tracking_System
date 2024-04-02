using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class AttendancePermission
    {
        [Key]
        public int AttendanceID { get; set; }
        public Attendance Attendance { get; set; }
        [Key]
        public int RequestID { get; set; }
        public PermissionRequest PermissionRequest { get; set; }
    }
}
