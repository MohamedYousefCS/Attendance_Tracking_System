using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class Instructor:User
    {

        public Role role { get; private set; } = Role.Instructor;

        [Range(5000, 25000, ErrorMessage = "Salary must be between 5000 and 25000.")]

        public decimal Salary { get; set; }


    }
}
