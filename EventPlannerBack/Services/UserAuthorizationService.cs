using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EventPlannerBack.Data.Entities;
using EventPlannerBack.Data.Repositories;
using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;
using EventPlannerBack.Services.HelperServices;

namespace EventPlannerBack.Services;

public class UserAuthorizationService(
    IUserRepository userRepository,
    IAuthCodesRepository authCodesRepository,
    IJWTTokenGenerator jwtTokenGenerator,
    IMailService mailService) : IUserAuthorizationService
{
    private static string GetHashedPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var sByte in bytes)
            {
                builder.Append(sByte.ToString("x2"));
            }

            return builder.ToString();
        }
    }

    public async Task<ResponseViewModel<UserSignUpResponse>> UserStartAuthentication(string email)
    {
        var generatedCode = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        var result = await mailService.SendAuthMail(email, "Authentication Code", generatedCode);
        if (result) {
            var guid = await authCodesRepository.CreateAuthCode(generatedCode, email);
            return new ResponseViewModel<UserSignUpResponse>()
            {
                Code = guid == string.Empty ? -1 : 0,
                Message = guid == string.Empty ? "Something went wrong" : "",
                Data = new UserSignUpResponse()
                {
                    UserEmailVerificationUniqueId = guid
                }
            };
        }
        return new ResponseViewModel<UserSignUpResponse>()
        {
            Code = -1,
            Message = "unable to send auth code to email",
            Data = null
        };
    }

    public async Task<ResponseViewModel<UserAuthentificationResponse>> EndAuthentication(string username,
        string password, string email, string code, string guid)
    {
        if (!await authCodesRepository.CheckAuthCode(code, email, guid))
            return new ResponseViewModel<UserAuthentificationResponse>()
            {
                Code = -1,
                Message = "Invalid code",
                Data = new UserAuthentificationResponse()
                {
                    Success = false,
                }
            };
        var result = await userRepository.AddUser(username, email, GetHashedPassword(password));
        if (result)
        {
            return new ResponseViewModel<UserAuthentificationResponse>()
            {
                Code = 0,
                Message = "e",
                Data = new UserAuthentificationResponse()
                {
                    Success = true,
                }
            };
        }

        return new ResponseViewModel<UserAuthentificationResponse>()
        {
            Code = -1,
            Message = "Something went wrong when adding user",
            Data = new UserAuthentificationResponse()
            {
                Success = false,
            }
        };
    }

    public async Task<ResponseViewModel<UserSingInResponse>> MakeUserSignIn(string identification, string password)
    {
        var result = await userRepository.CheckIfUser(identification, GetHashedPassword(password));
        if (result != null)
        {
            return new ResponseViewModel<UserSingInResponse>()
            {
                Code = 0,
                Message = "",
                Data = new UserSingInResponse()
                {
                    user = result,
                    JWTToken = jwtTokenGenerator.GenerateJWTtoken(new Claim[]
                        { new Claim(ClaimTypes.NameIdentifier, identification) }),
                }
            };
        }

        return new ResponseViewModel<UserSingInResponse>()
        {
            Code = -1,
            Message = "User Not Found"
        };
    }
}