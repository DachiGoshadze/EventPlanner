using System.Security.Claims;
using Application.Models;

namespace Application.Interfaces.Services;

public interface IJWTTokenGenerator
{
    string GenerateJWTtoken(IEnumerable<Claim> claims);
    UserModal GetClaimsFromToken(string token);
}