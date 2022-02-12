using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;

namespace Blazorcrud.Client.Services
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