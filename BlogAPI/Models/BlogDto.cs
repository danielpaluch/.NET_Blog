namespace BlogAPI.Models
{
    public class BlogDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Category { get; set; }
        public List<CommentDto> ?Comments {get;set;}
    }
}
