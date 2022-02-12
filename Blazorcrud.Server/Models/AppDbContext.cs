using Blazorcrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Blazorcrud.Server.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Person> People => Set<Person>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Upload> Uploads => Set<Upload>();
        public DbSet<User> Users => Set<User>();
    }
}