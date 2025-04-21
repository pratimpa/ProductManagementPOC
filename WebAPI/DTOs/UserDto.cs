using WebAPI.Models;

namespace WebAPI.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Token { get; set; }
        public string[] Roles { get; set; }
    }
}
