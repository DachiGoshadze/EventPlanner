using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EventPlannerBack.Data.Entities;

public class Event
{
    public int id { get; set; }
    
    public int UserId { get; set; }
    [DataType("nvarchar(64)")]
    public string eventName { get; set; }
    [DataType("nvarchar(64)")]
    public string eventDescription { get; set; }
    [DataType("datetime")]
    public DateTime startDate { get; set; }
    [DataType("datetime")]
    public DateTime endDate { get; set; }
    
    public List<EventsParticipantsQueue> participantsQueue { get; set; }
}