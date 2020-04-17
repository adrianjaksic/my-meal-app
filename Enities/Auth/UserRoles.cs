using System;
using System.Collections.Generic;
using System.Linq;

namespace Enities.Auth
{
    public class UserRoles
    {
        public const string User = "User";
        public const string Manager = "Manager";
        public const string Admin = "Admin";

        public const string ManagerOrAdmin = "Manager,Admin";

        public static bool CheckUserRole(LoginModel loggedUser, string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return true;
            }
            return roleName.Split(',').Any(x => x == loggedUser.RoleName);
        }

        public static bool ValidateUserRole(string roleName)
        {
            return roleName == User || roleName == Manager || roleName == Admin;
        }

        public static List<string> GetRoles()
        {
            return new List<string>() 
            {
                User,
                Manager,
                Admin
            };
        }
    }
}
