using Microsoft.AspNetCore.Http;

namespace Common.Exceptions
{
    public class NoContentException : BaseException
    {
        public NoContentException() : base("There is no content.", StatusCodes.Status404NotFound)
        {

        }

        public NoContentException(string message) : base(message, StatusCodes.Status404NotFound)
        {

        }
    }
}
