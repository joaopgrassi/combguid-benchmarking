using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ConsoleApp.EF
{
    public class CombGuidDbContext : DbContext
    {
        public CombGuidDbContext(
             DbContextOptions<CombGuidDbContext> options) : base(options)
        {
        }

        public DbSet<TableWithIdentity> TableWithIdentity { get; set; } = null!;

        public DbSet<TableWithRegularGuid> TableWithRegularGuid { get; set; } = null!;

        public DbSet<TableWithCombGuid> TableWithCombGuid { get; set; } = null!;

        public DbSet<TableWithRTCombGuid> TableWithRTCombGuid { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
