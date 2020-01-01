using BlazorCrud.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorCrud.Shared.Data
{
    public class ClaimContext : DbContext
    {
        public ClaimContext(DbContextOptions<ClaimContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
    }
}
