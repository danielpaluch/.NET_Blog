namespace BlogAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ?Nationality { get; set; }
        public DateTime ?DateOfBirth { get; set; }
        public DateTime? CreationOfAccount { get; set; } = DateTime.Now;

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
