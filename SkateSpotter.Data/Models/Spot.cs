namespace SkateSpotter.Data.Models
{
    public class Spot
    {
        public int SpotId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
