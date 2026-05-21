namespace SkateSpotter.Logic.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public int SpotId { get; set; }
    }
}