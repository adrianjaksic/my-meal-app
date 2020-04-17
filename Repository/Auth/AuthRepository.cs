using Common.Extensions;
using Enities.Auth;
using Interfaces.Auth;
using Microsoft.EntityFrameworkCore;
using Repository.Model;
using System;
using System.Linq;

namespace Repository.Auth
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public AuthRepository(DbContextOptions<DbMealsContext> options) : base(options) { }

        public bool CheckLogin(int userId, int logId)
        {
            using (var context = GetContext())
            {
                return context.UserLogs.Any(u => u.UserId == userId && u.LogId == logId && u.LogOutDate == null);
            }
        }

        public LoginModel LoginUser(string email, byte[] password, bool logOutOldLogs)
        {
            using (var context = GetContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email && u.Password == password);
                if (user == null)
                {
                    return null;
                }
                var today = DateTime.Now;
                if (logOutOldLogs)
                {
                    var oldLogs = context.UserLogs.Where(l => l.UserId == user.UserId && l.LogOutDate == null).ToList();
                    foreach (var item in oldLogs)
                    {
                        item.LogOutDate = today;
                    }
                }
                var logId = 1;
                if (context.UserLogs.Where(l => l.UserId == user.UserId).Any())
                {
                    logId = context.UserLogs.Where(l => l.UserId == user.UserId).Max(l => l.LogId + 1);
                }
                context.UserLogs.Add(new UserLog()
                {
                    UserId = user.UserId,
                    LogId = logId,
                    LogInDate = today,
                });
                context.SaveChanges();
                return new LoginModel()
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    LogId = logId,
                    FullName = user.FullName,
                    RoleName = user.RoleName,
                };
            }
        }

        public bool LogoffUser(int userId, int logId)
        {
            using (var context = GetContext())
            {
                var log = context.UserLogs.SingleOrDefault(l => l.UserId == userId && l.LogId == logId && l.LogOutDate == null);
                if (log == null)
                {
                    return false;
                }
                log.LogOutDate = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }
    }
}
