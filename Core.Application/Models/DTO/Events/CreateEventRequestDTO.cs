using Microsoft.AspNetCore.Http;

namespace Application.Models.DTO.Events;

public class CreateEventRequestDTO
{   
    public int UserId { get; set; }
    public string EventName { get; set; }
    public string EventDescription { get; set; }
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    
    public IFormFile EventParticipantEmails { get; set; }
}