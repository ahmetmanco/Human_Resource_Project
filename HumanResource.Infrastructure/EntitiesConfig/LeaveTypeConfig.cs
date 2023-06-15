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
	internal class LeaveTypeConfig : IEntityTypeConfiguration<LeaveType>
	{
		public void Configure(EntityTypeBuilder<LeaveType> builder)
		{

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id)
				.HasColumnOrder(1);

			builder.Property(x => x.Name)
				.IsRequired(true)
				.IsUnicode(true)
				.HasColumnType("nvarchar(30)")
				.HasColumnOrder(2);

			
		}
	}
}
