using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IUserRepository
    {
        bool RegisterUser(User user, string role);
        IEnumerable<User> GetAll();
        User GetUserByEmail(string email);
    }
}
