namespace EventPlannerBack.Models;

public class UserSignInRequest
{
    public string Identifier { get; set; }
    public string Password { get; set; }
}