using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public enum TrackStatus
    {
        Opened,
        Closed
    }
    public class Track
    {
        [Key]
        [Range(1000, 2000, ErrorMessage = "TrackId must be greater than or equal to 1000.")]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "TrackName is Required. ")]
        [MinLength(10, ErrorMessage = "TrackName must be at least 10 characters.")]
        [MaxLength(30, ErrorMessage = "TrackName cannot exceed 30 characters.")]
        public string TrackName { get; set; }

        public TrackStatus Status { get; set; }= TrackStatus.Opened;

        [Range(25, 50, ErrorMessage = "Capacity must be between 25 and 50.")]
        public int? Capacity { get; set; }

        //NV
        public int  programId {  get; set; }
        [ForeignKey("programId")]
        public virtual program program { get; set; } 
        //
        public int supervisorId { get; set; }

        [ForeignKey("supervisorId")]
        public virtual Instructor Instructor { get; set; }
        public virtual List<Instructor> Instructors { get; set; }=new List<Instructor>();
        public virtual List<Student> Students { get; set; }=new List<Student>();

        
    }
}
