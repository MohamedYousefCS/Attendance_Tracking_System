using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Instructor:User
    {

        public Instructor() { 
        
        Role=Role.Instructor;
        }

        [Range(5000, 25000, ErrorMessage = "Salary must be between 5000 and 25000.")]
        [Column(TypeName = "decimal(18, 2)")] // Adjust precision and scale as needed

        public decimal Salary { get; set; }
        //NV
        public virtual Track Track { get; set; }
        public virtual List<Track> Tracks { get; set; }=new List<Track>();
    }
}
