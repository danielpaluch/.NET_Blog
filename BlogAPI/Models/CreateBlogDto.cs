
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class CreateBlogDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Category { get; set; }
    }
}
