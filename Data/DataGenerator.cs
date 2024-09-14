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
            Console.WriteLine("We're getting there");
        }
    }
}