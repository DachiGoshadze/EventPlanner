namespace Application.Interfaces.Repositories;

public interface IAuthCodesRepository
{
    Task<string> CreateAuthCode(string code, string email);
    Task<bool> CheckAuthCode(string code, string email, string authGuid);
}