using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;

namespace Portfolio
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
