using FutMatch.Domain.Entities;
using FutMatch.Domain.Repositories;

namespace FutMatch.Infra.Data.Repositories;

public class AddressRepository : GenericRepository<Address>, IAddressRepository
{
    public AddressRepository(ApplicationDbContext context) : base(context)
    {
    }
}
