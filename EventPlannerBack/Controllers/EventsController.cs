using Application.Interfaces.Services;
using Application.Models;
using Application.Models.DTO.Events;
using Microsoft.AspNetCore.Mvc;

namespace EventPlannerBack.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class EventController(IEventService service, IJWTTokenGenerator tokenGenerator, IUserService userService)
{
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<CreateEventResponseDTO>> CreateEvent([FromForm]  CreateEventRequestDTO request )
    {
        try
        {
            var user = await userService.GetUserDefaultInfoAsync(request.UserId);
            return await service.CreateEventAsync(request, user);
        }
        catch (Exception ex)
        {
            return new ResponseViewModel<CreateEventResponseDTO>()
            {
                Code = -1,
                Message = ex.Message,
            };
        }
    }
    [HttpPut("[action]")]
    public async Task<bool> AddParticipentsToEvent([FromForm]  AddParticipentsRequestDTO request)
    {
        try
        {
            var res = await service.AddEventParticipantsAsync(request.file, request.EventId, request.Message);
            if (res)
            {
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<EventInfoResponseDTO>> GetEventInfo ([FromBody]  EventInfoRequestDTO request )
    {
        return await service.GetEventInfoAsync(request.EventId);
    }
}