namespace Enities.Users
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public byte[] Password { get; set; }
        public string RoleName { get; set; }
        public bool Active { get; set; }
        public bool ChangedRole { get; set; }
    }
}
