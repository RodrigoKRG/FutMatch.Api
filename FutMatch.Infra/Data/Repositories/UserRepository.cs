using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
    }
}
