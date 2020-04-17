using Enities.Auth;
using Enities.Users;
using System.Collections.Generic;

namespace Interfaces.Users
{
    public interface IUserService
    {
        int AddUser(LoginModel loggedInUser, UserRequest request);
        /// <summary>
        /// If looged in user role changed return true.
        /// </summary>
        bool EditUser(LoginModel loggedInUser, UserRequest request);
        List<UserModel> GetUsers(LoginModel loggedInUser);
        UserModel GetUser(LoginModel loggedInUser, int userId);
    }
}
