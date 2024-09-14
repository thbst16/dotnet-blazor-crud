using Bogus;

namespace BlazorCrud.Data
{
    public class DataGenerator
    {
        public static void Initialize(ApplicationDbContext appDbContext)
        {
            Randomizer.Seed = new Random(32321);
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
            if (!(appDbContext.People.Any()))
            {
                // create new people
                var testPeople = new Faker<Person>()
                    .RuleFor(p => p.FirstName, f => f.Name.FirstName())
                    .RuleFor(p => p.LastName, f => f.Name.LastName())
                    .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumberFormat());

                var people = testPeople.Generate(4);

                foreach (Person p in people)
                {
                    appDbContext.People.Add(p);
                }
                appDbContext.SaveChanges();
            }
        }
    }
}