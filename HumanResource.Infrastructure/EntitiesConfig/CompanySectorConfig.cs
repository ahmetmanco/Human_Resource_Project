using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    internal class CompanySectorConfig : IEntityTypeConfiguration<CompanySector>
    {
        public void Configure(EntityTypeBuilder<CompanySector> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasColumnType("nvarchar(50)")
                .HasColumnOrder(2);
        }
    }
}
