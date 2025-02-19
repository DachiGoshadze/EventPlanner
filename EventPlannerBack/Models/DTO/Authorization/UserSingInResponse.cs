namespace EventPlannerBack.Models;

public class UserSingInResponse
{
    public UserModal user {get; set;}
    public string JWTToken { get; set; }    
}