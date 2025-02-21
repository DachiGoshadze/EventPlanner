using Application.Models;
using Application.Models.DTO.Events;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Services;

public interface IEventService
{
    Task<ResponseViewModel<CreateEventResponseDTO>> CreateEventAsync(CreateEventRequestDTO createEventRequestDto, UserModal user);
    Task<ResponseViewModel<EventInfoResponseDTO>> GetEventInfoAsync(int eventId);
    Task<bool> AddEventParticipantsAsync(IFormFile file, int eventId, string message);
}