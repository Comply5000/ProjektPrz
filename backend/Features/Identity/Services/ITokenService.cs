using API.Features.Identity.Models;
using System.Security.Claims;

namespace API.Features.Identity.Services
{
    public interface ITokenService
    {
        JsonWebToken GenerateAccessTokenAsync(Guid userId, string userEmail, ICollection<string> roles, ICollection<Claim> claims);
    }
}
