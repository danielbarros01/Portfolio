using Microsoft.EntityFrameworkCore;
using Portfolio.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portfolio
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TechnicalSkill> TechnicalSkills { get; set; }
    }
}
