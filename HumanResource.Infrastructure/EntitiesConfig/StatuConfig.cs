using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    internal class StatuConfig : IEntityTypeConfiguration<Statu>
    {
        public void Configure(EntityTypeBuilder<Statu> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired(true);

            //Foreign Key

            builder.HasMany(x => x.AppUsers)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Leaves)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Advances)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Addresses)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Departments)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.ExpenseTypes)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);            
            
            builder.HasMany(x => x.LeaveTypes)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(x => x.Titles)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Expenses)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Companies)
               .WithOne(x => x.Statu)
               .HasForeignKey(x => x.StatuId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
