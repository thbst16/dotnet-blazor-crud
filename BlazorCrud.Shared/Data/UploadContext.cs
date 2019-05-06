using BlazorCrud.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Shared.Data
{
    public class UploadContext : DbContext
    {
        public UploadContext(DbContextOptions<UploadContext> options)
            : base(options)
        {
        }

        public DbSet<Upload> Uploads { get; set; }
    }
}
