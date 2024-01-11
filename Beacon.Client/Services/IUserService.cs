using Beacon.Client.Shared;
using Beacon.Shared.Data;
using Beacon.Shared.Models;

namespace Beacon.Client.Services
{
    public interface IUserService
    {
        User User {get; }
        Task Initialize();
        Task Login(Login model);
        Task Logout();
        Task<PagedResult<User>> GetUsers(string? name, string page);
        Task<User> GetUser(int id);
        Task DeleteUser(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
    }
}