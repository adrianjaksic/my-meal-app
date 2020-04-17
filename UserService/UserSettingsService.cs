using Common.Exceptions;
using Enities.Auth;
using Enities.Users;
using Interfaces.Users;

namespace UserService
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IUserSettingsRepository _userSettingsRepository;

        public UserSettingsService(IUserSettingsRepository userSettingsRepository)
        {
            _userSettingsRepository = userSettingsRepository;
        }

        public void EditSettings(LoginModel loggedInUser, UserSettingsModel request)
        {
            CheckPermission(loggedInUser, request.UserId, "Can't edit someone else settings.");
            _userSettingsRepository.EditSettings(request);
        }

        public UserSettingsModel GetSettings(LoginModel loggedInUser, int userId)
        {
            CheckPermission(loggedInUser, userId, "Can't get someone else settings.");
            var settings = _userSettingsRepository.GetSettings(userId);            
            return settings ?? new UserSettingsModel() { UserId = userId };
        }

        private static void CheckPermission(LoginModel loggedInUser, int mealUserId, string message)
        {
            var isAdmin = UserRoles.CheckUserRole(loggedInUser, UserRoles.Admin);
            if (!isAdmin && mealUserId != loggedInUser.UserId)
            {
                throw new UnauthorizedException(message);
            }
        }
    }
}
