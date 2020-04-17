using Microsoft.AspNetCore.Http;

namespace Common.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException() : base("Unauthorized access.", StatusCodes.Status401Unauthorized)
        {

        }

        public UnauthorizedException(string message) : base(message, StatusCodes.Status401Unauthorized)
        {

        }
    }
}
