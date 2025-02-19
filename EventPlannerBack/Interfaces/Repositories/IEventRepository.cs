using EventPlannerBack.Data.Entities;

namespace EventPlannerBack.Interfaces.Repositories;

public interface IEventRepository
{
    Task<int> CreateEventAsync(Event newEvent);
    Task<Event?> GetEventAsync(int id);
    Task<bool> AddEventParticipantsAsync(int eventId, string message, List<string> participantsEmails);
}