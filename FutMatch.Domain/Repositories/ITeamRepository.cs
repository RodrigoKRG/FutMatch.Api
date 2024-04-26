using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Repositories;

public interface ITeamRepository : IGenericRepository<Team>
{
    Task<List<Player>> GetPlayers(long id);
}
