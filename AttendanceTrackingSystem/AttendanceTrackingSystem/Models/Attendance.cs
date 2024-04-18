using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public enum Status
    {
        Present,
        Absent,
        Late
    }

    public enum PermissionType
    {
        None,
        Excused,
        Unexcused
    }

    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

       
        public DateOnly Date { get; set; }

        public TimeOnly? TimeIn { get; set; }

        public TimeOnly? TimeOut { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public Status  Status { get; set; }


        public bool? IsPermission { get; set; }

        public PermissionType? PermissionType { get; set; }
        //NV
        public  int userId { get; set; }

        [ForeignKey("userId")]
        public  virtual User User { get; set; }

    }
}
