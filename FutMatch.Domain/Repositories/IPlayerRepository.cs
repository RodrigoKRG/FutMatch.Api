using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Repositories;

public interface IPlayerRepository : IGenericRepository<Player>
{
    Task<bool> ExistsByCpfAsync(string cpf);
}
