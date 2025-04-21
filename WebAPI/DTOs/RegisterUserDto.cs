using WebAPI.Models;

namespace WebAPI.DTOs
{
    public class RegisterUserDto
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public List<string> UserRoles { get; set; }

    }
}
