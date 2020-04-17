using Enities.Auth;
using Enities.Users;

namespace Interfaces.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers and login user.
        /// </summary>
        /// <param name="model">Register data</param>
        /// <returns>Login data</returns>
        LoginModel RegisterUser(RegisterRequest model);
        /// <summary>
        /// Login the user.
        /// </summary>
        /// <param name="email">User email</param>
        /// <param name="password">User password</param>
        /// <returns>Login data</returns>
        LoginModel LoginUser(string email, string password);
        /// <summary>
        /// Invalidate the logid of the user.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="logId">User log id</param>
        void LogoffUser(int userId, int logId);
        bool CheckLogin(int userId, int logId);
    }
}
