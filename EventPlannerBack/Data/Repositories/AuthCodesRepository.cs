using EventPlannerBack.Data.Entities;
using EventPlannerBack.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventPlannerBack.Data.Repositories;

public class AuthCodesRepository(ApplicationContext context) : IAuthCodesRepository
{
    private ApplicationContext _context = context;

    public async Task<string> CreateAuthCode(string code, string email)
    {
        try
        {
            var newGuid = Guid.NewGuid();
            var auth = new AuthenticationCodes()
            {
                Code = code,
                AuthGuid = newGuid.ToString(),
                Email = email
            };
            await _context.AuthCodes.AddAsync(auth);
            await _context.SaveChangesAsync();
            return newGuid.ToString();
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public async Task<bool> CheckAuthCode(string code, string email, string authGuid)
    {
        try
        {
            var cd = await _context.AuthCodes.FirstOrDefaultAsync(x => x.AuthGuid == authGuid);
            return cd != null && cd.Email == email && cd.Code == code;
        }
        catch (Exception)
        {
            return false;
        }
    }
}