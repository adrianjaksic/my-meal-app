namespace Enities.Auth
{
    public class LoginModel : BaseModel
    {
        public int UserId { get; set; }
        public int LogId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
