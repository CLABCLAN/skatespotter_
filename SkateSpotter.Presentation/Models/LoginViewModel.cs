using System.ComponentModel.DataAnnotations;

namespace SkateSpotter.Presentation.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is verplicht")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Wachtwoord is verplicht")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}