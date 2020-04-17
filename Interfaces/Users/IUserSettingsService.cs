using Enities.Auth;
using Enities.Users;

namespace Interfaces.Users
{
    public interface IUserSettingsService
    {
        void EditSettings(LoginModel loggedInUser, UserSettingsModel request);
        UserSettingsModel GetSettings(LoginModel loggedInUser, int userId);
    }
}
