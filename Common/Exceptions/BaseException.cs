using Microsoft.AspNetCore.Http;
using System;

namespace Common.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCode { get; set; }

        public BaseException(string message) : base(message)
        {
            StatusCode = StatusCodes.Status500InternalServerError;
        }

        public BaseException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
