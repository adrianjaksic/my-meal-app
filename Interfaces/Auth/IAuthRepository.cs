using Enities.Auth;

namespace Interfaces.Auth
{
    public interface IAuthRepository
    {
        LoginModel LoginUser(string email, byte[] password, bool logOutOldLogs);
        bool LogoffUser(int userId, int logId);
        bool CheckLogin(int userId, int logId);
    }
}
