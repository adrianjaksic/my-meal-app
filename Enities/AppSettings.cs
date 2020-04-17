namespace Enities
{
    public class AppSettings
    {
        public string AuthSecret { get; set; }
        public string ClientUrl { get; set; }
        public bool UseRequestLogging { get; set; }
        public string DbConnectionString { get; set; }
        public bool LogOutOldLogs { get; set; }
        public string PasswordSalt { get; set; }
        public bool Live { get; set; }
        public int AuthTokenExpiresInHours { get; set; }
    }
}
