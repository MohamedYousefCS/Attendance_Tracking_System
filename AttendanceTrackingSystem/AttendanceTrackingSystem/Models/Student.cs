using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Student:User
    {

        public Student()
        {
            Role=Role.Student;
        }

        [Required(ErrorMessage = "University is required.")]
        [MinLength(8, ErrorMessage = "University must be at least 8 characters.")]
        [MaxLength(20, ErrorMessage = "University cannot exceed 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-]*$", ErrorMessage = "Invalid Name")]

        public string University { get; set; }

        [Required(ErrorMessage = "Faculty is Required. ")]
        [MinLength(5, ErrorMessage = "Faculty must be at least 5 characters.")]
        [MaxLength(20, ErrorMessage = " Faculty cannot exceed 20 characters.")]
        [RegularExpression(@"^[a-zA-Z\s\-]*$", ErrorMessage = "Invalid Name")]

        public string Faculty { get; set; }

        [Required(ErrorMessage = "Specialization is Required. ")]
        [MinLength(2, ErrorMessage = "Specialization must be at least 2 characters.")]
        [MaxLength(80, ErrorMessage = " Specialization cannot exceed 80 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Invalid Name")]

        public string Specialization { get; set; }

        [Required(ErrorMessage = "GraduationYear is Required. ")]
        [Range(2014, int.MaxValue, ErrorMessage = "Graduation year must be between 2015 and the current year.")]
        public int GraduationYear { get; set; }

        //NV
        public int trackId { get; set; }

        [ForeignKey("trackId")]
        public virtual Track Track { get; set; }

        public virtual List<PermissionRequest> PermissionRequests { get; set; } = new List<PermissionRequest>();



    }
}
