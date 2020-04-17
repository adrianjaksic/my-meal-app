namespace Enities.Auth
{
    public class RegisterRequest : BaseRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
