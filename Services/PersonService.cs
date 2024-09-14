using BlazorCrud.Data;

namespace BlazorCrud.Services
{
    public class PersonService: IPersonService
    {
        private readonly ApplicationDbContext _dbContext;
        public PersonService(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public void DeletePerson(int id)
        {
            var person = _dbContext.People.FirstOrDefault(x => x.Id == id);
            if (person!=null)
            {
                _dbContext.People.Remove(person);
                _dbContext.SaveChanges();
            }
        }
        public Person GetPersonById(int id)
        {
            var person = _dbContext.People.SingleOrDefault(x => x.Id == id);
            if (person == null)
            {
                throw new Exception("Person for ID: " + id + " not found.");
            }
            return person;
        }
        public List<Person> GetPeople()
        {
            return _dbContext.People.ToList();
        }
        public void SavePerson(Person person)
        {
            if (person.Id == 0) _dbContext.People.Add(person);
            else _dbContext.People.Update(person);
            _dbContext.SaveChanges();
        }
    }
}