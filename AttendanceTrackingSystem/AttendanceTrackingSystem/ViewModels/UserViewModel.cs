using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.ViewModels
{
    public class UserViewModel
    {
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please enter your email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
        
        public long Id { get; set; }

        public string Role { get; set; }

        public string Name { get; set; }

        public string ErrorMessage { get; set; }
    }
}
