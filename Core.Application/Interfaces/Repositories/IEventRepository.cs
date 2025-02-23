using Application.Models;

namespace Application.Interfaces.Repositories;

public interface IEventRepository
{
    Task<int> CreateEventAsync(EventModal newEvent);
    Task<EventModal?> GetEventAsync(int id);
    Task<List<EventModal>?> GetUserEventsAsync(int id);
    Task<bool> AddEventParticipantsAsync(int eventId, string message, List<string> participantsEmails);
}