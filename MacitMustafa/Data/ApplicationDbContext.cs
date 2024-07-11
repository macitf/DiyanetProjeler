using Microsoft.EntityFrameworkCore;
using MacitMustafa.Models;

namespace MacitMustafa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Il> Iller { get; set; }
        public DbSet<Ilce> Ilceler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ilce varlığını anahtarsız olarak tanımlamak için:
            modelBuilder.Entity<Ilce>().HasNoKey();
        }
    }
}
