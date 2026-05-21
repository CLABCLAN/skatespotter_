using System.ComponentModel.DataAnnotations;

namespace SkateSpotter.Presentation.Models
{
    public class SpotCreateViewModel
    {
        [Required(ErrorMessage = "Naam is verplicht")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Latitude is verplicht")]
        public decimal Latitude { get; set; }

        [Required(ErrorMessage = "Longitude is verplicht")]
        public decimal Longitude { get; set; }
    }
}