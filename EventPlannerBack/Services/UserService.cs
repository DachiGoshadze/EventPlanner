using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;

namespace EventPlannerBack.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserModal> GetUserDefaultInfoAsync(int userId)
    {
        var user = await userRepository.GetUserInfo(userId);
        if (user == null)
        {
            throw new Exception($"User with id {userId} not found");
        }

        return user;
    }
}