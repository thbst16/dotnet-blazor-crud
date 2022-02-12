namespace Blazorcrud.Server.Authorization;

public class AuthenticateRequest
{
    public string Username {get; set;} = default!;
    public string Password { get; set; } = default!;
}