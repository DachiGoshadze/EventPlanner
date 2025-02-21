using Application.Models;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserModal?> CheckIfUser(string identifier, string password);
    Task<bool> AddUser(string username, string email, string password);
    Task<UserModal?> GetUserInfo(int userId);
}