using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Repositories;

public interface IFieldPositionRepository : IGenericRepository<FieldPosition>
{
    Task<List<FieldPosition>> GetListByIdsAsync(List<long> ids);
}
