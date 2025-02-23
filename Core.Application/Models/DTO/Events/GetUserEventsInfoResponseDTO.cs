namespace Application.Models.DTO.Events;

public class GetUserEventsInfoResponseDTO
{
    public ICollection<EventInfoResponseDTO> EventInfos { get; set; }
}