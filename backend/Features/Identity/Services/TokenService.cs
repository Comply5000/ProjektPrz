using API.Features.Identity.Config;
using API.Features.Identity.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Features.Identity.Services
{
    public sealed class TokenService : ITokenService
    {
        private readonly AuthConfig _authConfig;
        private readonly SigningCredentials _signingCredentials;

        public TokenService(AuthConfig authConfig)
        {
            _authConfig = authConfig;
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.JwtKey)),
                SecurityAlgorithms.HmacSha256);
        }

        public JsonWebToken GenerateAccessTokenAsync(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims)
        {
            var now = DateTime.UtcNow;
            var issuer = _authConfig.JwtIssuer;

            var jwtClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
            };

            if (roles?.Any() is true)
                foreach (var role in roles)
                    jwtClaims.Add(new Claim("role", role));

            if (claims?.Any() is true)
            {
                var customClaims = new List<Claim>();
                foreach (var claim in claims)
                {
                    customClaims.Add(new Claim(claim.Type, claim.Value));
                }
                jwtClaims.AddRange(customClaims);
            }

            var expires = now.Add(_authConfig.Expires);

            var jwt = new JwtSecurityToken(
                issuer,
                issuer,
                jwtClaims,
                now,
                expires,
                _signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
                UserId = userId,
                Email = userEmail,
                Roles = roles,
                Claims = claims?.ToDictionary(x => x.Type, x => x.Value)
            };
        }
    }
}
