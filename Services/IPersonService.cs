using BlazorCrud.Data;

namespace BlazorCrud.Services
{
    public interface IPersonService
    {
        List<Person> GetPeople();
        Person GetPersonById(int id);
        void SavePerson(Person person);
        void DeletePerson(int id);
    }
}