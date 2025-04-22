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

        public bool RegisterUser(User user)
        {
            var userRoleNames = user.Roles.Select(r => r.RoleName.ToLower()).ToList();

            // Get existing roles
            var existingRoles = _db.Roles
                .Where(r => userRoleNames.Contains(r.RoleName.ToLower()))
                .ToList();

            // Find missing roles
            var missingRoleNames = userRoleNames
                .Except(existingRoles.Select(r => r.RoleName.ToLower()))
                .ToList();

            // Add missing roles to the database
            foreach (var roleName in missingRoleNames)
            {
                var newRole = new Role { RoleName = roleName };
                _db.Roles.Add(newRole);
                existingRoles.Add(newRole); // Add it to the list to assign to user later
            }

            if (existingRoles.Any())
            {
                user.Roles = existingRoles;
                _db.Users.Add(user);
                _db.SaveChanges();
                return true;
            }

            return false;
           
        }
    }
}
