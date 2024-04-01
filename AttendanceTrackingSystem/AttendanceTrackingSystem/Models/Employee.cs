using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public class Employee:User
    {

        public Role role { get; private set; } = Role.Employee;


        [Range(2000, 10000, ErrorMessage = "Salary must be between 5000 and 25000.")]

        public decimal Salary { get; set; }
    }
}
