using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    

    public class Program
    {
        [Key]
        [Range(100, 1000, ErrorMessage = "TrackId must be greater than or equal to 100.")]

        public int ProgramId { get; set; }

        [Required(ErrorMessage = "ProgramName is Required. ")]
        [MinLength(10, ErrorMessage = "ProgramName must be at least 10 characters.")]
        [MaxLength(50, ErrorMessage = "ProgramName cannot exceed 50 characters.")]
        public string ProgramName { get; set; }

        public Intake Intake { get; set; }

      


    }
}
