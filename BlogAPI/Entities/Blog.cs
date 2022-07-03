namespace BlogAPI.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int ?CreatedById { get; set; }
        public virtual User CreatedByUser { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Comment> ?Comments { get; set; }
    }
}
