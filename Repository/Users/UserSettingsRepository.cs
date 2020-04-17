using Enities.Users;
using Interfaces.Users;
using Microsoft.EntityFrameworkCore;
using Repository.Model;
using System.Linq;

namespace Repository.Users
{
    public class UserSettingsRepository : BaseRepository, IUserSettingsRepository
    {
        public UserSettingsRepository(DbContextOptions<DbMealsContext> options) : base(options) { }

        public void EditSettings(UserSettingsModel settings)
        {
            using (var context = GetContext())
            {
                var dbSettings = context.UserMealSettings.SingleOrDefault(ums => ums.UserId == settings.UserId);
                if (dbSettings == null)
                {
                    dbSettings = new UserMealSettings()
                    {
                        UserId = settings.UserId,
                    };
                    context.UserMealSettings.Add(dbSettings);
                }
                dbSettings.DailyNumberOfCalories = settings.DailyNumberOfCalories;
                context.SaveChanges();
            }
        }

        public UserSettingsModel GetSettings(int userId)
        {
            using (var context = GetContext())
            {
                return context.UserMealSettings.AsNoTracking().Where(ums => ums.UserId == userId).Select(r => new UserSettingsModel()
                {
                    UserId = userId,
                    DailyNumberOfCalories = r.DailyNumberOfCalories,
                }).SingleOrDefault();
            }
        }
    }
}
