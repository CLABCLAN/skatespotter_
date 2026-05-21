namespace SkateSpotter.Data.DTOs
{
    public class SpotDto
    {
        public int SpotId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int CreatedUserId { get; set; }
        public string CreatedByUsername { get; set; } = string.Empty;

        public List<ReviewDto> Reviews { get; set; } = new();
    }
}