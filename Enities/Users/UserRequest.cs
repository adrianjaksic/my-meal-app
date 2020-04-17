namespace Enities.Users
{
    public class UserRequest : BaseRequest
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
        public int UserId { get; set; }
    }
}
