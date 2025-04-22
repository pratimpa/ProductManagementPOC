using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Repositories.Implementations
{
    public class UserRepository: IUserRepository
    {
        ProductMgrAuthDbContext _db;
        public UserRepository(ProductMgrAuthDbContext db)
        {
            _db = db;
        }
        public IEnumerable<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.Include(u => u.Roles).FirstOrDefault(x => x.Email == email);
        }

        public bool RegisterUser(User user, string role)
        {
            Role roleData = _db.Roles.Where(x => x.RoleName == role).FirstOrDefault();
            if ((roleData != null))
            {
                user.Roles.Add(roleData);
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
