using EventPlannerBack.Data.Entities;
using EventPlannerBack.Interfaces.Repositories;
using EventPlannerBack.Interfaces.Services;
using EventPlannerBack.Models;
using EventPlannerBack.Models.DTO.Events;
using OfficeOpenXml;

namespace EventPlannerBack.Services;

public class EventService(IEventRepository eventRepository) : IEventService
{
    public async Task<ResponseViewModel<CreateEventResponseDTO>> CreateEventAsync(CreateEventRequestDTO createEventRequestDto, UserModal user)
    {
        var response =  new ResponseViewModel<CreateEventResponseDTO>()
        {
            Code = -1,
            Message = "Event Create Error",
        };
        var eventId = await eventRepository.CreateEventAsync(new Event()
        {
            UserId = user.Id,
            eventName = createEventRequestDto.EventName,
            eventDescription = createEventRequestDto.EventDescription,
            startDate = createEventRequestDto.EventStartDate,
            endDate = createEventRequestDto.EventEndDate
        });
        if (eventId < 0)
            return response;
        
        List<string> emails = new List<string>();
        using (var stream = new MemoryStream())
        {
            await createEventRequestDto.EventParticipantEmails.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; 

                if (worksheet == null)
                    return response;

                int rowCount = worksheet.Dimension?.Rows ?? 0;

                for (int row = 1; row <= rowCount; row++)
                {
                    var cellValue = worksheet.Cells[row, 1].Text; 
                    if (!string.IsNullOrEmpty(cellValue))
                        emails.Add(cellValue);
                }
            }
        }
        var result = await eventRepository.AddEventParticipantsAsync(eventId, createEventRequestDto.EventDescription, emails);
        if (!result) return response;
        return new ResponseViewModel<CreateEventResponseDTO>()
        {
            Code = 0,
            Message = "",
            Data = new CreateEventResponseDTO { EventId = eventId }
        };
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