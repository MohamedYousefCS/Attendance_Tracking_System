using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Intake
    {
        [Key]
        [Range(44, 100, ErrorMessage = "IntakeNumber must be between 44 and 100.")]
        public int IntakeNumber { get; set; }

        [Required(ErrorMessage = "IntakeName is Required. ")]
        [MinLength(5, ErrorMessage = "IntakeName must be at least 5 characters.")]
        [MaxLength(10, ErrorMessage = "IntakeName cannot exceed 10 characters.")]
        public string IntakeName { get; set; }

        //NV
        public int ProgramId { get; set; }
        [ForeignKey("ProgramId ")]
        public virtual program program { get; set; }    




    }
}
