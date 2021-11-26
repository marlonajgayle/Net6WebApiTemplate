namespace Net6WebApiTemplate.Infrastructure.Identity
{
    public class IdentityOptions
    {
        public bool RequiredDigit { get; set; }
        public int RequiredLength { get; set; }
        public bool RequireLowercase { get; set; }
        public int RequiredUniqueChars { get; set; }
        public bool RequireUppercase { get; set; }
        public int MaxFailedAttempts { get; set; }
        public int LockoutTimeSpanInDays { get; set; }
    }
}