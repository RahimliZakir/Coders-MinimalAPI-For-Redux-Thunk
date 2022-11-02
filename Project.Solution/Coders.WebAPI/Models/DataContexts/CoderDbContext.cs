using Coders.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coders.WebAPI.Models.DataContexts
{
    public class CoderDbContext : DbContext
    {
        public CoderDbContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Coder> Coders { get; set; }
    }
}
