using EventPlannerBack.Data.Entities;
using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Models.DTO.Events;
using Microsoft.EntityFrameworkCore;

namespace EventPlannerBack.Data.Repositories;

public class EventRepository(ApplicationContext applicationContext) : IEventRepository
{
    public async Task<int> CreateEventAsync(Event newEvent)
    {
        try
        {
            await applicationContext.Events.AddAsync(newEvent);
            await applicationContext.SaveChangesAsync();
            return newEvent.id;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public async Task<Event?> GetEventAsync(int id)
    {
        try
        {
            var ent = await applicationContext.Events.FirstOrDefaultAsync(x => x.id == id);
            return ent;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AddEventParticipantsAsync(int eventId, string message, List<string> participantsEmails)
    {
        try
        {
            foreach (var email in participantsEmails)
            {
                await applicationContext.eventsParticipantsQueue.AddAsync(new EventsParticipantsQueue()
                {
                    EventId = eventId,
                    Message = message,
                    ParticipantEmail = email,
                    SendStatus = 0
                });
            }
            await applicationContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}