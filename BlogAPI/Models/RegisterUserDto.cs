
using System.ComponentModel.DataAnnotations;
namespace BlogAPI.Entities
{
    public class RegisterUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(7)]
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int RoleId { get; set; } = 1;
    }
}
