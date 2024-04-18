using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{

    public enum RequestType
    {
        Late,
        Absence
    }

    public enum IsAccepted
    {
        pending,
        Rejected,
        Accepted
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

        public string Reason { get; set; }

        [Required(ErrorMessage = "IsAccepted is required.")]
        public IsAccepted IsAccepted { get; set; }

        [Required(ErrorMessage = "Type is required.")]
        public RequestType Type { get; set; }


        //NV
        public int studentId { get; set; }
        [ForeignKey("studentId")]
        public virtual Student Student { get; set; }




    }
}