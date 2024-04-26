using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using FutMatch.Infra.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<TEntity> _set;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _set = context.Set<TEntity>();
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<List<TEntity>> GetAllAsync() =>
        await _set.ToListAsync();

    public async Task<TEntity?> GetByIdAsync(long id) =>
        await _set.FindAsync(id);

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public Task<bool> RemoveAsync(TEntity entity)
    {
        _set.Remove(entity);
        return _context.SaveChangesAsync().ContinueWith(task => task.Result > 0);
    }

    public async Task<TEntity?> GetByIdAsync(long id, string[] includes) =>
        await PrivateGetByIdAsync(id, includes);

    private async Task<TEntity?> PrivateGetByIdAsync(long id, string[]? includes) =>
        await _set.AsNoTracking().IncludeMultiple(includes).SingleOrDefaultAsync(entity => entity.Id == id);
}
