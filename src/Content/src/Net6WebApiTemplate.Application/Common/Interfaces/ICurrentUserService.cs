namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        public string UserId { get; }
        bool IsAuthenticated { get; }
        public string IpAddress { get; }
    }
}