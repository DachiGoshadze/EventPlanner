using Application.Models;
using Application.Models.DTO.Authorization;

namespace Application.Interfaces.Services;

public interface IUserAuthorizationService
{
    Task<ResponseViewModel<UserSignUpResponse>> UserStartAuthentication(string email);

    Task<ResponseViewModel<UserAuthentificationResponse>> EndAuthentication(string username,
        string password, string email, string code, string guid);

    Task<ResponseViewModel<UserSingInResponse>> MakeUserSignIn(string identification, string password);
}