using HumanResource.Domain.Entities;
using HumanResource.Domain.Repositries;
using HumanResource.Infrastructure.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource.Infrastructure.Repositories
{
    public class BloodTypeRepository : BaseRepository<BloodType>, IBloodTypeRepository
    {
        public BloodTypeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
