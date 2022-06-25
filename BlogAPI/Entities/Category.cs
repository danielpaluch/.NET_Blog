namespace BlogAPI.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
