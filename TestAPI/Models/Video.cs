using TestAPI.Enum;

namespace TestAPI.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string? Source { get; set; }
        public string? Image { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public VideoType Tag { get; set; }
    }
}