using Blazorcrud.Server.Authorization;
using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Server.Models
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
        PagedResult<User> GetUsers(string? name, int page);
        Task<User?> GetUser(int Id);
        Task<User> AddUser(User user);
        Task<User?> UpdateUser(User user);
        Task<User?> DeleteUser(int id);
    }
}