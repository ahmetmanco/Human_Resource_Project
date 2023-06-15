using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositories;
using HumanResource.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Infrastructure.Repositories
{
	public class CurrencyTypeRepository : BaseRepository<CurrencyType>, ICurrencyTypeRepository
	{
		public CurrencyTypeRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
