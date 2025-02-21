using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class AuthenticationCodes
{
    public int id { get; set; }
    [DataType("nvarchar(64)")]
    public string AuthGuid { get; set; }
    [DataType("nvarchar(64)")]
    public string Email { get; set; }
    [DataType("nvarchar(64)")]
    public string Code { get; set; }
}