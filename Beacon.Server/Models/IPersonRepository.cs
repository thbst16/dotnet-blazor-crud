using Beacon.Shared.Data;
using Beacon.Shared.Models;

namespace Beacon.Server.Models
{
    public interface IPersonRepository
    {
        PagedResult<Person> GetPeople(string? name, int page);
        Task<Person?> GetPerson(int personId);
        Task<Person> AddPerson(Person person);
        Task<Person?> UpdatePerson(Person person);
        Task<Person?> DeletePerson(int personId);
    }
}