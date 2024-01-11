namespace Beacon.Server.Authorization;
using Beacon.Shared.Models;

public class AuthenticateResponse
{
    public int Id {get; set;}
    public string FirstName {get; set;}
    public string LastName {get; set;}
    public string Username {get;set;}    
    public string Token { get; set; } = default!;
}