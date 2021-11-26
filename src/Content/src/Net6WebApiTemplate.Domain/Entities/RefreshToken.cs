namespace Net6WebApiTemplate.Domain.Entities
{
    public class RefreshToken
    {
        public string JwtId { get; set; }
        public string Token { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
        public DateTime? Revoked { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
        public string RemoteIpAddress { get; set; }
    }
}