using Enities.Users;

namespace Interfaces.Users
{
    public interface IUserSettingsRepository
    {
        void EditSettings(UserSettingsModel settings);
        UserSettingsModel GetSettings(int userId);
    }
}
