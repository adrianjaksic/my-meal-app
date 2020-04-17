using Enities.Users;
using Microsoft.EntityFrameworkCore;
using Repository.Model;
using System.Collections.Generic;
using System.Linq;
using Interfaces.Users;
using Common.Exceptions;
using Enities.Auth;

namespace Repository.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(DbContextOptions<DbMealsContext> options) : base(options) { }

        public int AddUser(UserModel user)
        {
            using (var context = GetContext())
            {
                var dbUser = context.Users.SingleOrDefault(u => u.Email == user.Email);
                if (dbUser != null)
                {
                    throw new BadArgumentException("User with provided email existis.");
                }
                dbUser = new User()
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    RoleName = user.RoleName,
                    Password = user.Password,                    
                    Active = true,
                };
                context.Users.Add(dbUser);
                context.SaveChanges();
                return dbUser.UserId;
            }
        }

        public void EditUser(UserModel user)
        {
            using (var context = GetContext())
            {
                var dbUser = context.Users.SingleOrDefault(u => u.UserId == user.UserId);
                if (dbUser != null)
                {
                    dbUser.FullName = user.FullName;
                    dbUser.RoleName = user.RoleName;
                    dbUser.Active = user.Active;
                    context.SaveChanges();
                }
            }
        }

        public UserModel GetUser(int userId)
        {
            using (var context = GetContext())
            {
                return context.Users.AsNoTracking().Where(u => u.UserId == userId).Select(u => new UserModel()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    RoleName = u.RoleName,
                    Active = u.Active,
                }).SingleOrDefault();
            }
        }

        public UserModel GetUserByEmail(string email)
        {
            using (var context = GetContext())
            {
                return context.Users.AsNoTracking().Where(u => u.Email == email).Select(u => new UserModel()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    RoleName = u.RoleName,
                    Active = u.Active,
                }).SingleOrDefault();
            }
        }

        public List<UserModel> GetUsers(bool withAdmin)
        {
            using (var context = GetContext())
            {
                var query = context.Users.AsNoTracking();

                if (!withAdmin)
                {
                    query = query.Where(u => u.RoleName != UserRoles.Admin);
                }

                return query.Select(u => new UserModel() 
                { 
                    UserId = u.UserId,
                    Email = u.Email,
                    FullName = u.FullName,
                    RoleName = u.RoleName,
                    Active = u.Active,
                }).ToList();
            }
        }
    }
}
