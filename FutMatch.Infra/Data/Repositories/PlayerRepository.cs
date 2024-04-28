using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data.Repositories;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<Player> _set;

    public PlayerRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
        _set = context.Set<Player>();
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        return await _set.AnyAsync(player => player.Cpf == cpf);  
    }

    public async Task<Player?> GetByUserId(long userId)
    {
        return await _set.FirstOrDefaultAsync(player => player.UserId == userId);
    }
}
