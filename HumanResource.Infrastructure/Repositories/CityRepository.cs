using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    public class CityRepository : BaseRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
