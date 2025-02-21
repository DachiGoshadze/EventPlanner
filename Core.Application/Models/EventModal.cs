namespace Application.Models;

public class EventModal
{
    public int id { get; set; }
    public int UserId { get; set; }
    public string eventName { get; set; }
    public string eventDescription { get; set; }
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    
    public List<EventsParticipantsQueueModal> participantsQueue { get; set; }
}