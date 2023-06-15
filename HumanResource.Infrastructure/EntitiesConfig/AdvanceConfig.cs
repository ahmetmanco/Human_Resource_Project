using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    internal class AdvanceConfig : BaseEntityConfig<Advance>
    {
        public override void Configure(EntityTypeBuilder<Advance> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnOrder(1);

            builder.Property(x => x.Amount)
                .IsRequired(true)
                .HasColumnOrder(2);

            builder.Property(x => x.NumberOfInstallments)
                .HasColumnOrder(3);

            builder.Property(x=>x.Description)
                .IsRequired(true)
                .HasColumnType("NVARCHAR(MAX)")
                .HasColumnOrder (4);

            builder.Property(x=>x.AdvanceDate)
                .IsRequired(true)
                .HasColumnType("date")
                .HasColumnOrder (5);

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnOrder(6);

            builder.HasOne(x => x.User) // Advance in appuser ile ilişkisi
                .WithMany(x => x.Advances)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);



            base.Configure(builder);

        }
    }
}
