using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class EventsParticipantsQueue
{
    public int Id { get; set; }
    [Required]
    public int EventId { get; set; }
    
    public string Message { get; set; }
    
    [Required]
    public string ParticipantEmail { get; set; }

    public int SendStatus { get; set; } = 0;
}