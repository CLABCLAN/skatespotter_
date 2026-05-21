namespace SkateSpotter.Logic.Models
{
    public class Spot
    {
        public int SpotId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedByUsername { get; set; } = string.Empty;
        public List<Review> Reviews { get; set; } = new();

        public double GemRating =>
            Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    }
}