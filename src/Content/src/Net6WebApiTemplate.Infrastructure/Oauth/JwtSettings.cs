namespace Net6WebApiTemplate.Infrastructure.Oauth
{
    public class JwtSettings
    {
        public string Secret { get; set; }
        public TimeSpan Expiration { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public bool ValidateIssuer { get; set; }
        public string Issuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string Audience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; }
        public int RefreshTokenLifetime { get; set; }
    }
}