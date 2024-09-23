namespace Blazorcrud.Server.Authorization;
using Blazorcrud.Shared.Models;

public class AuthenticateResponse
{
    public int Id {get; set;}
    public string FirstName {get; set;} = null!;
    public string LastName {get; set;} = null!;
    public string Username {get;set;} = null!;
    public string Token { get; set; } = default!;
}