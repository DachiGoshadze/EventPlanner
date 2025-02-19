namespace EventPlannerBack.Models.DTO.Events;

public class EventParticipantViewModel
{
    public int id { get; set; }
    public int eventId { get; set; }
    public string participantEmail { get; set; }
    public int status { get; set; }
}