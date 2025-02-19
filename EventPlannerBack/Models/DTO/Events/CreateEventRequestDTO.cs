namespace EventPlannerBack.Models.DTO.Events;

public class CreateEventRequestDTO
{   
    public string EventName { get; set; }
    public string EventDescription { get; set; }
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    
    public IFormFile EventParticipantEmails { get; set; }
}