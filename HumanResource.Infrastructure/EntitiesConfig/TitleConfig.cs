using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    public class TitleConfig : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.Name)
                .IsRequired(true)
                .IsUnicode(true)
                .HasColumnType("nvarchar(50)")
                .HasColumnOrder(2);

            builder.HasMany(x => x.Users)
                .WithOne(x => x.Title)
                .HasForeignKey(x => x.TitleId);
        }
    }
}
