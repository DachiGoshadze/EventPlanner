using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;
using EventPlannerBack.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPlannerBack.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UserAuthorizationController(IUserAuthorizationService service)
{
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<UserSignUpResponse>> SignUp([FromBody]  UserSignUpRequest request )
    {
            return await service.UserStartAuthentication(request.Email);
    }
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<UserSingInResponse>> SignIn ([FromBody]  UserSignInRequest request )
    {
            return await service.MakeUserSignIn(request.Identifier, request.Password);
    }
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<UserAuthentificationResponse>> UserAuthenticate([FromBody] UserAuthentificationCoreRequest request)
    {
            return await service.EndAuthentication(request.Username, request.Password, request.Email, request.AuthentificationCode, request.Guid);
    }
}