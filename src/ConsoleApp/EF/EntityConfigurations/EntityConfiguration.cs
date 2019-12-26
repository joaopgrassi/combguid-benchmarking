using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.EF.EntityConfigurations
{
    public class TableWithRegularGuidConfiguration : BaseBenchmarkEntityConfiguration<TableWithRegularGuid>
    {
    }

    public class TableWithExtendedUuidCreateSequentialConfiguration : BaseBenchmarkEntityConfiguration<TableWithExtendedUuidCreateSequential>
    {
    }

    public class TableWithNewSequentialIdAsDefaultConfiguration : BaseBenchmarkEntityConfiguration<TableWithNewSequentialIdAsDefault>
    {
        public override void Configure(EntityTypeBuilder<TableWithNewSequentialIdAsDefault> builder)
        {
            base.Configure(builder);

            // Configures the columns with the default SQL Sequential Id Generation
            builder.Property(p => p.Id).HasDefaultValueSql("NewSequentialId()");
            builder.Property(p => p.AnotherId).HasDefaultValueSql("NewSequentialId()");
        }
    }

    public class TableWithSpanCustomGuidCombConfiguration : BaseBenchmarkEntityConfiguration<TableWithSpanCustomGuidComb>
    {
    }

    public class TableWithCustomGuidInSqlConfiguration : BaseBenchmarkEntityConfiguration<TableWithCustomGuidInSql>
    {
        public override void Configure(EntityTypeBuilder<TableWithCustomGuidInSql> builder)
        {
            base.Configure(builder);

            // Configures the columns with the default SQL Sequential Id Generation
            builder.Property(p => p.Id).HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid(),(0))+CONVERT([binary](6),getutcdate(),(0)),(0)))");
            builder.Property(p => p.AnotherId).HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid(),(0))+CONVERT([binary](6),getutcdate(),(0)),(0)))");
        }
    }

    public class BaseBenchmarkEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseBenchmarkEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.AnotherId).IsRequired();

            builder.Property(p => p.Value).IsRequired().HasMaxLength(400);

            // NONCLUSTERED INDEX
            builder.HasIndex(p => new { p.AnotherId, p.Value });
        }
    }
}
