using Beacon.Shared.Data;
using Beacon.Shared.Models;

namespace Beacon.Client.Services
{
    public interface IPersonService
    {
        Task<PagedResult<Person>> GetPeople(string? name, string page);
        Task<Person> GetPerson(int id);

        Task DeletePerson(int id);

        Task AddPerson(Person person);

        Task UpdatePerson(Person person);
    }
}