using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HumanResource.Infrastructure.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<AppUser> GetDefault(Expression<Func<AppUser, bool>> expression)
        {
            return await _table.Include(x=>x.Address).FirstOrDefaultAsync(expression);
        }
    }
}
