using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    public class AppUserConfig : BaseEntityConfig<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id).HasColumnOrder(1);

            builder.Property(x => x.FirstName)
                .IsRequired(true)
                .HasMaxLength(30)
                .IsUnicode(true)
                .HasColumnOrder(2);

            builder.Property(x => x.LastName)
                .IsRequired(true)
                .HasMaxLength(50)
                .IsUnicode(true)
                .HasColumnOrder(3);

            builder.Property(x=>x.BirthDate)
                .IsRequired(false)
                .HasColumnType("date")
                .HasColumnOrder(4);

            //builder.Property(x => x.AddressId)
            //    .IsRequired(true)
            //    .HasColumnOrder(5);

            builder.Property(x => x.DepartmentId)
                .IsRequired(false)
                .HasColumnOrder(6);

            builder.Property(x => x.BloodTypeId)
                .IsRequired(false)
                .HasColumnOrder(7);

            builder.Property(x => x.ManagerId)
                .IsRequired(false)
                .HasColumnOrder(8);

            builder.Property(x => x.UserName)
                .HasMaxLength(30);


            //Foreign Key
            builder.HasOne(x=>x.Manager)
                    .WithMany(x=>x.Employees)
                    .HasForeignKey(x=>x.ManagerId);

            builder.HasOne(x => x.BloodType)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.BloodTypeId);

            builder.HasMany(x => x.Expenses)
                    .WithOne(x => x.User)
                    .HasForeignKey(x => x.UserId);


            base.Configure(builder);
        }
    }
}
