using WebAPI.Models;

namespace WebAPI.Services.Abstractions
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
