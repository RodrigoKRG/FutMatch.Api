using FutMatch.Domain.Entities;

namespace FutMatch.Domain.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByLoginAsync(string login);
    }
}
