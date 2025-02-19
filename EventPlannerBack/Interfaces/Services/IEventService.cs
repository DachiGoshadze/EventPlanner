using EventPlannerBack.Models;
using EventPlannerBack.Models.DTO.Events;

namespace EventPlannerBack.Interfaces.Services;

public interface IEventService
{
    Task<ResponseViewModel<CreateEventResponseDTO>> CreateEventAsync(CreateEventRequestDTO createEventRequestDto, UserModal user);
    Task<ResponseViewModel<EventInfoResponseDTO>> GetEventInfoAsync(int eventId);
}