using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Models
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int RoleId { get; set; } = 1;
    }
}
