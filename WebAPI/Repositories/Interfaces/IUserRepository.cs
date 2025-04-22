using WebAPI.Models;

namespace WebAPI.Repositories
{
    public interface IUserRepository
    {
        bool RegisterUser(User user);
        IEnumerable<User> GetAll();
        User GetUserByEmail(string email);
    }
}
