using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{

    public enum RequestType
    {
        Late,
        Absence
    }
    public class PermissionRequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        [MinLength(10)]
        [MaxLength(100)]
        [RegularExpression(@"^(?![0-9])[a-zA-Z\s]*$", ErrorMessage = "Invalid Reason")]

        public string Reason { get; set; }

        [Required(ErrorMessage = "IsAccepted is required.")]
        public bool IsAccepted { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public RequestType Type { get; set; }

        //NV
        public int userId {  get; set; }
        [ForeignKey("userId")]
        public virtual User User { get; set; }
        public ICollection<AttendancePermission> AttendancePermissions { get; set; } = new HashSet<AttendancePermission>();

        //public virtual List<Attendance> Attendances { get; set; }= new List<Attendance>();



    }
}
