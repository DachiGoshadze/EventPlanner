using Application.Models;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<UserModal> GetUserDefaultInfoAsync(int userId);
}