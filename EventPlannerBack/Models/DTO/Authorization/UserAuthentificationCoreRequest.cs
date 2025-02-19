namespace EventPlannerBack.Models;

public class UserAuthentificationCoreRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string AuthentificationCode { get; set; }
    public string Guid { get; set; }
}