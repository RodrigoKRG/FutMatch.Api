using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data.Repositories;

public class TeamRepository : GenericRepository<Team>, ITeamRepository
{
    public TeamRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<Player>> GetPlayers(long id)
    {
        return await _context.Teams
            .Include(x => x.Players)
            .Where(x => x.Id == id)
            .SelectMany(x => x.Players)
            .ToListAsync();
    }
}
