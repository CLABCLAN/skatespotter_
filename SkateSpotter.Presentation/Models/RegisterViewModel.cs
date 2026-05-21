using System.ComponentModel.DataAnnotations;

namespace SkateSpotter.Presentation.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is verplicht")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [MinLength(6, ErrorMessage = "Wachtwoord moet minimaal 6 tekens zijn")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public string? Name { get; set; }
    }
}