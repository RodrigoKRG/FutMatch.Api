using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data.Repositories
{
    public class FieldPositionRepository : GenericRepository<FieldPosition>, IFieldPositionRepository
    {
        public FieldPositionRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<List<FieldPosition>> GetListByIdsAsync(List<long> ids) =>
            _set.Where(x => ids.Contains(x.Id)).ToListAsync();
    }
}
