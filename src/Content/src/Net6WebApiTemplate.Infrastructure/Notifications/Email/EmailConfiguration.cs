namespace Net6WebApiTemplate.Infrastructure.Notifications.Email
{
    public class EmailConfiguration
    {
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
    }
}