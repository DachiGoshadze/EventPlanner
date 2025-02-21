namespace Application.Models;

public class EventsParticipantsQueueModal
{
    public int Id { get; set; }
    public int EventId { get; set; }
    
    public string Message { get; set; }

    public string ParticipantEmail { get; set; }

    public int SendStatus { get; set; } = 0;
}