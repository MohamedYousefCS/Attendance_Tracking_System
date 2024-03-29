using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.Models
{
    public abstract class User
    {
        [Key]
       public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string Mobile { get; set; }

        public string DateOfBirth { get; set; }





    }
}
