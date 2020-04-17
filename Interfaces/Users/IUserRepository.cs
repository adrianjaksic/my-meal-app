using Enities.Users;
using System.Collections.Generic;

namespace Interfaces.Users
{
    public interface IUserRepository
    {
        int AddUser(UserModel model);
        void EditUser(UserModel model);
        List<UserModel> GetUsers(bool withAdmins);
        UserModel GetUser(int userId);
        UserModel GetUserByEmail(string email);
    }
}
