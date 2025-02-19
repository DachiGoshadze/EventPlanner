namespace EventPlannerBack.Models.DTO.Events;

public class EventInfoResponseDTO
{
    public int EventId { get; set; }
    public string EventName { get; set; }
    public string EventDescription { get; set; }
    public DateTime EventStartDate { get; set; }
    public DateTime EventEndDate { get; set; }
    public ICollection<EventParticipantViewModel> EventParticipants { get; set; } = new List<EventParticipantViewModel>();
}