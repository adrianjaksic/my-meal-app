using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class SecurityHelper
    {
        public static byte[] HashPassword(string stringToHash)
        {
            if (!string.IsNullOrEmpty(stringToHash))
            {
                byte[] buffer = Encoding.Default.GetBytes(stringToHash);
                using (var cryptoTransformSHA1 = new SHA1CryptoServiceProvider())
                {
                    return cryptoTransformSHA1.ComputeHash(buffer);
                }
            }
            return null;
        }        
    }
}
