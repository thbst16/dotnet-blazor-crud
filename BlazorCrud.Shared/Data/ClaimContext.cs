using BlazorCrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Shared.Data
{
    public class ClaimContext : DbContext
    {
        public ClaimContext(DbContextOptions<ClaimContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
    }
}
