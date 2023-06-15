using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;

namespace HumanResource.Infrastructure.Repositories
{
    public class LeaveRepository : BaseRepository<Leave>, ILeaveRepository
    {
        public LeaveRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
