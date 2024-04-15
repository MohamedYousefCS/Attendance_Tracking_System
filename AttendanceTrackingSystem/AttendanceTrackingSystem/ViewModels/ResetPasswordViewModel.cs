using System.ComponentModel.DataAnnotations;

namespace AttendanceTrackingSystem.ViewModels
{
    public class ResetPasswordViewModel : IValidatableObject
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "New password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Password must be at least 8 characters long contains at least one uppercase letter, one digit, and one special character.")]
        public string NewPassword { get; set; } = "";

        [Required(ErrorMessage = "Confirm password is required")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (NewPassword != null && ConfirmPassword != null && NewPassword != ConfirmPassword)
            {
                yield return new ValidationResult("The new password and confirm password do not match.", [nameof(ConfirmPassword)]);
            }
        }
    }
}
