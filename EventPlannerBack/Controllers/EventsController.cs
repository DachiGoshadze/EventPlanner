using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;
using EventPlannerBack.Models.DTO.Events;
using EventPlannerBack.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlannerBack.Controllers;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
[Authorize]
public class EventController(IEventService service, IJWTTokenGenerator tokenGenerator, IHttpContextAccessor _httpContextAccessor)
{
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<CreateEventResponseDTO>> CreateEvent([FromForm]  CreateEventRequestDTO request )
    {
        var user = tokenGenerator.GetClaimsFromToken(_httpContextAccessor.HttpContext!.Request.Headers["Authorization"].ToString());
        return await service.CreateEventAsync(request, user);
    }
    
    [HttpPost("[action]")]
    public async Task<ResponseViewModel<EventInfoResponseDTO>> GetEventInfo ([FromBody]  EventInfoRequestDTO request )
    {
        return await service.GetEventInfoAsync(request.EventId);
    }
}