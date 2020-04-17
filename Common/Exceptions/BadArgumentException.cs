using Microsoft.AspNetCore.Http;

namespace Common.Exceptions
{
    public class BadArgumentException : BaseException
    {
        public BadArgumentException() : base("Arguments not valid.", StatusCodes.Status400BadRequest)
        {

        }

        public BadArgumentException(string message) : base(message, StatusCodes.Status400BadRequest)
        {

        }
    }
}
