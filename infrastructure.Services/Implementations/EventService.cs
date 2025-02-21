using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Models;
using Application.Models.DTO.Events;
using ClosedXML.Excel;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace infrastructure.Services.Implementations;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<ResponseViewModel<CreateEventResponseDTO>> CreateEventAsync(CreateEventRequestDTO createEventRequestDto, UserModal user)
    {
        var response =  new ResponseViewModel<CreateEventResponseDTO>()
        {
            Code = -1,
            Message = "Event Create Error",
        };
        var eventId = await eventRepository.CreateEventAsync(new EventModal()
        {
            UserId = user.Id,
            eventName = createEventRequestDto.EventName,
            eventDescription = createEventRequestDto.EventDescription,
            startDate = createEventRequestDto.EventStartDate,
            endDate = createEventRequestDto.EventEndDate
        });
        if (eventId <= 0)
            return response;

        var result = await AddEventParticipantsAsync(createEventRequestDto.EventParticipantEmails, eventId,
            createEventRequestDto.EventDescription);
        if (!result) return response;
        return new ResponseViewModel<CreateEventResponseDTO>()
        {
            Code = 0,
            Message = "",
            Data = new CreateEventResponseDTO { EventId = eventId }
        };
    }

    public async Task<bool> AddEventParticipantsAsync(IFormFile file, int eventId, string message)
    {
        using var workbook = new XLWorkbook(file.OpenReadStream());
        var worksheet = workbook.Worksheet(1);
        var emails = worksheet.Column("A").CellsUsed().Skip(1).Select(cell => cell.GetValue<string>()).ToList();
        return await eventRepository.AddEventParticipantsAsync(eventId, message, emails);
    }
    public async Task<ResponseViewModel<EventInfoResponseDTO>> GetEventInfoAsync(int eventId)
    {
        var eventInfo = await eventRepository.GetEventAsync(eventId);
        if (eventInfo == null)
        {
            return new ResponseViewModel<EventInfoResponseDTO>()
            {
                Code = -1,
                Message = "Event Not Found"
            };
        }
        
        return new ResponseViewModel<EventInfoResponseDTO>()
        {
            Code = 0,
            Data = new EventInfoResponseDTO
            {
                EventId = eventInfo.id,
                EventName = eventInfo.eventName,
                EventDescription = eventInfo.eventDescription,
                EventStartDate = eventInfo.startDate,
                EventEndDate = eventInfo.endDate,
                EventParticipants = eventInfo.participantsQueue.Select(x => new EventParticipantViewModel()
                {
                    eventId = x.EventId,
                    id = x.Id,
                    participantEmail = x.ParticipantEmail,
                    status = x.SendStatus
                }).ToList()
            }
        };
    }
}