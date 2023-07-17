using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class SearchRequest
    {
        [Required]
        public string? Title { get; set; }
    }
}
