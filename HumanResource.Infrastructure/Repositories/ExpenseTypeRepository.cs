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
	public class ExpenseTypeRepository : BaseRepository<ExpenseType>, IExpenseTypeRepository
	{
		public ExpenseTypeRepository(ApplicationDbContext context) : base(context)
		{

		}
	}
}
