namespace Enities.Exceptions
{
    public class ExceptionResponse : BaseModel
    {
        public string ErrorMessage { get; set; }
        public string RequestUrl { get; set; }
    }
}
