using Application.Interfaces.Services;
using Application.Models;
using Application.Models.DTO.UserInfo;
using Microsoft.AspNetCore.Mvc;

namespace EventPlannerBack.Controllers;


[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService)
{
    [HttpGet("GetUserInfo")]
    public async Task<ResponseViewModel<GetUserInfoResponseDTO>> GetUserInfo([FromQuery] GetUserInfoDTO getUserInfo)
    {
        try
        {
            var result = await userService.GetUserDefaultInfoAsync(getUserInfo.UserId);
            return new ResponseViewModel<GetUserInfoResponseDTO>()
            {
                Code = 0,
                Message = "",
                Data = new GetUserInfoResponseDTO()
                {
                    UserId = result.Id,
                    Username = result.Username,
                    Email = result.Email
                }
            };
        }
        catch (Exception e)
        {
            return new ResponseViewModel<GetUserInfoResponseDTO>()
            {
                Code = -1,
                Message = "User not found",
            };
        }
    }
    
}