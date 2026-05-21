using System.ComponentModel.DataAnnotations;

namespace SkateSpotter.Presentation.Models
{
    public class ReviewCreateViewModel
    {
        [Required]
        public int SpotId { get; set; }

        [Required(ErrorMessage = "Rating is verplicht")]
        [Range(1, 5, ErrorMessage = "Rating moet tussen 1 en 5 zijn")]
        public int Rating { get; set; }

        public string? Comment { get; set; }
    }
}