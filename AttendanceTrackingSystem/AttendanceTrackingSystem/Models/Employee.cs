using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTrackingSystem.Models
{
    public class Employee:User
    {

        public Role role { get; private set; } = Role.Employee;


        [Range(2000, 10000, ErrorMessage = "Salary must be between 5000 and 25000.")]
        [Column(TypeName = "decimal(18, 2)")] // Adjust precision and scale as needed

        public decimal Salary { get; set; }
    }
}
