using Application.Interfaces.Repositories;
using Application.Models;
using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2024.PivotAutoRefresh;
using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Repositories;

public class EventRepository(ApplicationContext applicationContext) : IEventRepository
{
    public async Task<int> CreateEventAsync(EventModal newEvent)
    {
        try
        {
            var ev = new Event()
            {
                UserId = newEvent.UserId,
                eventName = newEvent.eventName,
                eventDescription = newEvent.eventDescription,
                startDate = newEvent.startDate,
                endDate = newEvent.endDate
            };
            await applicationContext.Events.AddAsync(ev);
            await applicationContext.SaveChangesAsync();
            return ev.id;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public async Task<EventModal?> GetEventAsync(int id)
    {
        try
        {
            var ent = await applicationContext.Events.FirstOrDefaultAsync(x => x.id == id);
            if (ent == null) return null;
            return new EventModal()
            {
                id = ent.id,
                eventName = ent.eventName,
                eventDescription = ent.eventDescription,
                startDate = ent.startDate,
                endDate = ent.endDate,
                participantsQueue = ent.participantsQueue.Select(x => new EventsParticipantsQueueModal()
                {
                    EventId = x.EventId,
                    Id = x.Id,
                    Message = x.Message,
                    ParticipantEmail = x.ParticipantEmail,
                    SendStatus = x.SendStatus
                }).ToList(),
            };
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

    public async Task<List<EventModal>?> GetUserEventsAsync(int id)
    {
        try
        {
            var userEvents = await applicationContext.Events.Where(x => x.UserId == id).Select(x => new EventModal()
            {
                id = x.id,
                eventName = x.eventName,
                eventDescription = x.eventDescription,
                startDate = x.startDate,
                endDate = x.endDate,
                participantsQueue = x.participantsQueue.Select(t => new EventsParticipantsQueueModal()
                {
                    EventId = t.EventId,
                    Id = t.Id,
                    Message = t.Message,
                    ParticipantEmail = t.ParticipantEmail,
                    SendStatus = t.SendStatus
                }).ToList(),
            }).ToListAsync();
            return userEvents;
        }
        catch (Exception)
        {
            return null;
        }
    }
}