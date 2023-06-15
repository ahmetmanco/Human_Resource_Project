using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositories;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    internal class CompanySectorRepository : BaseRepository<CompanySector>, ICompanySectorRepository
    {
        public CompanySectorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
