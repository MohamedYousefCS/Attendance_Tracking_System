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

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "TimeIn is required.")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime TimeIn { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? TimeOut { get; set; }

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
