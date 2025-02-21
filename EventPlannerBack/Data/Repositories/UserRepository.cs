using EventPlannerBack.Data.Entities;
using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlannerBack.Data.Repositories;

public class UserRepository(ApplicationContext context) : IUserRepository
{
    private readonly ApplicationContext _context = context;
    
    public async Task<UserModal?> CheckIfUser(string identifier, string password)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                (x.email == identifier || x.username == identifier) && x.password == password);
            if (user == null) return null;
            return new UserModal()
            {
                Id = user.id,
                Email = user.email,
                Username = user.username,
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddUser(string username, string email, string password)
    {
        try
        {
            var user = new User()
            {
                email = email,
                username = username,
                password = password
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<UserModal?> GetUserInfo(int userId)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.id == userId);
            if (user == null) return null;
            return new UserModal()
            {
                Id = user.id,
                Username = user.username,
                Email = user.email,
            };
        }
        catch
        {
            return null;
        }
    }
}