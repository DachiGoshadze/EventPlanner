using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models;

namespace infrastructure.Services.Implementations;

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