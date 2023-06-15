using HumanResource.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Infrastructure.EntitiesConfig
{
    public class ExpenseConfig : BaseEntityConfig<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasColumnOrder(1);

            builder.Property(x => x.ExpenseType)
                .IsRequired(true)
                .HasColumnOrder(2);

            builder.Property(x => x.UserId)
                .IsRequired(true)
                .HasColumnOrder(3);

            builder.Property(x => x.ShortDescription)
                .IsRequired(true)
                .HasColumnOrder(4)
                .HasMaxLength(30);
                

            builder.Property(x => x.LongDescription)
                .IsRequired(false)
                .HasColumnOrder(5)
                .HasColumnType("NVARCHAR(MAX)");

            builder.Property(x => x.ExpenseDate)
                .IsRequired(true)
                .HasColumnType("Date")
                .HasColumnOrder(6);

            builder.Property(x => x.Amount)
                .IsRequired(true)
                .HasColumnOrder(7);

            builder.Property(x => x.CurrencyTypeId)
                .IsRequired(true)
                .HasColumnOrder(8);

            builder.Property(x => x.ExpenseTypeId)
                .IsRequired(true)
                .HasColumnOrder(9);

            builder.HasOne(x => x.CurrencyType)
                .WithMany(x => x.Expenses)
                .HasForeignKey(x => x.CurrencyTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ExpenseType)
                .WithMany(x => x.Expenses)
                .HasForeignKey(x => x.ExpenseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            base.Configure(builder);
            
        }
    }
}
