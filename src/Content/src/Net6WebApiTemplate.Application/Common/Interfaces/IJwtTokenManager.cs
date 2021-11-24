using Net6WebApiTemplate.Application.Auth;
using System.Security.Claims;

namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface IJwtTokenManager
    {
        Task<AuthResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken);
        Task<ClaimsPrincipal> GetPrincipFromTokenAsync(string token);
    }
}