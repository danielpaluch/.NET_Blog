﻿namespace BlogAPI.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
