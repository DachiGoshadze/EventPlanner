using EventPlannerBack.Models;

namespace EventPlannerBack.Interfaces.Services;

public interface IUserAuthorizationService
{
    Task<ResponseViewModel<UserSignUpResponse>> UserStartAuthentication(string email);

    Task<ResponseViewModel<UserAuthentificationResponse>> EndAuthentication(string username,
        string password, string email, string code, string guid);

    Task<ResponseViewModel<UserSingInResponse>> MakeUserSignIn(string identification, string password);
}