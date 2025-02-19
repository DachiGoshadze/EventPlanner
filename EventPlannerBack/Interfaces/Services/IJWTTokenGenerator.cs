using System.Security.Claims;
using EventPlannerBack.Models;

namespace EventPlannerBack.Interfaces.Services;

public interface IJWTTokenGenerator
{
    string GenerateJWTtoken(IEnumerable<Claim> claims);
    UserModal GetClaimsFromToken(string token);
}