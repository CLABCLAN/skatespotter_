namespace SkateSpotter.Data.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int SpotId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}