using Microsoft.AspNetCore.Http;

namespace Application.Models.DTO.Events;

public class AddParticipentsRequestDTO
{
    public int EventId { get; set; }
    public string Message { get; set; }
    
    public IFormFile file {get; set;}
}