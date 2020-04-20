using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ConsoleApp.EF.EntityConfigurations
{
    public class TableWithIdentityConfiguration : BaseBenchmarkEntityConfiguration<TableWithIdentity, int>
    {
    }

    public class TableWithRegularGuidConfiguration : BaseBenchmarkEntityConfiguration<TableWithRegularGuid, Guid>
    {
    }

    public class TableWithCombGuidConfiguration : BaseBenchmarkEntityConfiguration<TableWithCombGuid, Guid>
    {
    }

    public class TableWithRTCombGuidConfiguration : BaseBenchmarkEntityConfiguration<TableWithRTCombGuid, Guid>
    {
    }

    public class BaseBenchmarkEntityConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseBenchmarkEntity<TId>
        where TId: struct
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Value).IsRequired().HasMaxLength(400);

            // NONCLUSTERED INDEX
            builder.HasIndex(p => p.Value);
        }
    }
}
