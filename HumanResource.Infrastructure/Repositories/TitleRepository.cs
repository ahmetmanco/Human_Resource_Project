using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositories;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    public class TitleRepository : BaseRepository<Title>, ITitleRepository
    {
        public TitleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
