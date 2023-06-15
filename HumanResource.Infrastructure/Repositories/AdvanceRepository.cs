using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    public class AdvanceRepository : BaseRepository<Advance>, IAdvanceRepository
    {
        public AdvanceRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
