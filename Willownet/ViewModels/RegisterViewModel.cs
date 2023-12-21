using System.ComponentModel.DataAnnotations;

namespace Willownet.ViewModels
{
    public class RegisterViewModel : IValidatableObject
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is invalid")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
                ErrorMessage = "Password is too simple")]
        public string? Password { get; set; }

        // Controller => ModelState.IsValid =>
        // Error for field "Password"
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Password == "qwerty")
            {
                yield return new ValidationResult("Password is too simple", new[] { "Password" });
            }
        }
    }
}
