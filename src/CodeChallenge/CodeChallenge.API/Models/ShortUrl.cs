using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.API.Models
{
    public class ShortUrls
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }

        public int Clicked { get; set; }
    }
}
