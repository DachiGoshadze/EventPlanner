using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;

namespace EventPlannerBack.Services.HelperServices
{

    public class JwtTokenGenerator : IJWTTokenGenerator
    {
        public string GenerateJWTtoken(IEnumerable<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
           issuer: AuthOptions.ISSUER,
           audience: AuthOptions.AUDIENCE,
           claims: claims,
           expires: DateTime.UtcNow.Add(TimeSpan.FromHours(2)),
           signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
           return $"Bearer {new JwtSecurityTokenHandler().WriteToken(jwt)}";
        }
        public UserModal GetClaimsFromToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token cannot be null or empty.");
            }

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, 
                    ValidateAudience = true, 
                    ValidateLifetime = true,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);


                return new UserModal
                {
                    Id = int.Parse(principal.FindFirst("userId")!.Value),
                    Username = principal.FindFirst("username")!.Value,
                    Email = principal.FindFirst("email")!.Value,
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to validate token.", ex);
            }
        }
    }
}
