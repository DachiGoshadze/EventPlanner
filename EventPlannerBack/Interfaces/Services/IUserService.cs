using EventPlannerBack.Models;

namespace EventPlannerBack.Interfaces.Services;

public interface IUserService
{
    Task<UserModal> GetUserDefaultInfoAsync(int userId);
}