using System.Security.Claims;

namespace EventPlannerBack.Interfaces.Services;

public interface IJWTTokenGenerator
{
    string GenerateJWTtoken(IEnumerable<Claim> claims);
    ClaimsPrincipal GetClaimsFromToken(string token);
}