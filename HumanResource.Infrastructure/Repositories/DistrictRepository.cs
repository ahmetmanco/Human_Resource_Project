using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
