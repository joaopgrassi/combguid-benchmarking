using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConsoleApp.EF
{
    public class CombGuidDbContext : DbContext
    {
        public DbSet<TableWithRegularGuid> TableWithRegularGuid { get; set; }
        public DbSet<TableWithNewSequentialIdAsDefault> TableWithNewSequentialIdAsDefault { get; set; }
        public DbSet<TableWithExtendedUuidCreateSequential> TableWithExtendedUuidCreateSequential { get; set; }
        public DbSet<TableWithSpanCustomGuidComb> TableWithSpanCustomGuidComb { get; set; }
        public DbSet<TableWithCustomGuidInSql> TableWithCustomGuidInSql { get; set; }
        public DbSet<TableWithVbCombGuid> TableWithVbCombGuid { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=CombGuidBenchmark;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
