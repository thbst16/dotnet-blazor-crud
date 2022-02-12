using Blazorcrud.Shared.Data;
using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class PersonRepository:IPersonRepository
    {
        private readonly AppDbContext _appDbContext;

        public PersonRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Person> AddPerson(Person person)
        {
            var result = await _appDbContext.People.AddAsync(person);
            await _appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Person?> DeletePerson(int personId)
        {
            var result = await _appDbContext.People.FirstOrDefaultAsync(p => p.PersonId==personId);
            if (result!=null)
            {
                _appDbContext.People.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
            return result;
        }

        public async Task<Person?> GetPerson(int personId)
        {
            var result = await _appDbContext.People
                .Include(p => p.Addresses)
                .FirstOrDefaultAsync(p => p.PersonId==personId);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
        }

        public PagedResult<Person> GetPeople(string? name, int page)
        {
            int pageSize = 5;
            
            if (name != null)
            {
                return _appDbContext.People
                    .Where(p => p.FirstName.Contains(name, StringComparison.CurrentCultureIgnoreCase) ||
                        p.LastName.Contains(name, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(p => p.PersonId)
                    .Include(p => p.Addresses)
                    .GetPaged(page, pageSize);
            }
            else
            {
                return _appDbContext.People
                    .OrderBy(p => p.PersonId)
                    .Include(p => p.Addresses)
                    .GetPaged(page, pageSize);
            }
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            var result = await _appDbContext.People.Include("Addresses").FirstOrDefaultAsync(p => p.PersonId==person.PersonId);
            if (result!=null)
            {
                // Update existing person
                _appDbContext.Entry(result).CurrentValues.SetValues(person);
                
                // Remove deleted addresses
                foreach (var existingAddress in result.Addresses.ToList())
                {
                   if(!person.Addresses.Any(o => o.AddressId == existingAddress.AddressId))
                     _appDbContext.Addresses.Remove(existingAddress);
                }

                // Update and Insert Addresses
                 foreach (var addressModel in person.Addresses)
                 {
                    var existingAddress = result.Addresses
                        .Where(a => a.AddressId == addressModel.AddressId)
                        .SingleOrDefault();
                    if (existingAddress != null)
                        _appDbContext.Entry(existingAddress).CurrentValues.SetValues(addressModel);
                    else
                    {
                        var newAddress = new Address
                        {
                            AddressId = addressModel.AddressId,
                            Street = addressModel.Street,
                            City = addressModel.City,
                            State = addressModel.State,
                            ZipCode = addressModel.ZipCode
                        };
                        result.Addresses.Add(newAddress);
                    }
                }
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Person not found");
            }
            return result;
        }
    }
}