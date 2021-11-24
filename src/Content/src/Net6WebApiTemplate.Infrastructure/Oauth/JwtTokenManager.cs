using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Net6WebApiTemplate.Application.Auth;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Infrastructure.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net6WebApiTemplate.Infrastructure.Oauth
{
    public class JwtTokenManager : IJwtTokenManager
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtTokenManager(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager, TokenValidationParameters tokenValidationParameters)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public async Task<AuthResult> GenerateClaimsTokenAsync(string username, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(username);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {

                    new Claim(ClaimTypes.NameIdentifier, user.Id), // TODO: encrypt user id for added security
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(5)).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var refreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id), // TODO: encrypt user id for added security
                    new Claim(ClaimTypes.Name, username),
                    new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create JWT tokens
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshtoken = tokenHandler.CreateToken(refreshTokenDescriptor);

            return new AuthResult
            {
                AccessToken = tokenHandler.WriteToken(token),
                TokenType = "Bearer",
                ExpiresIn = _jwtSettings.Expiration.Seconds,
                RefreshToken = tokenHandler.WriteToken(refreshtoken)
            };
        }

        public async Task<ClaimsPrincipal> GetPrincipFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // disable token lifetime validation as we are validating against an expired token.
                var tokenValdationParams = _tokenValidationParameters.Clone();
                tokenValdationParams.ValidateLifetime = false;

                var principal = tokenHandler.ValidateToken(token, tokenValdationParams, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return await Task.Run(() => principal);
            }
            catch
            {
                return null;
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
        }
    }
}