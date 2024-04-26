using FutMatch.Domain.Entities;
using FutMatch.Infra.Data.Mappings;

using Microsoft.EntityFrameworkCore;

namespace FutMatch.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<FieldPosition> FieldPositions { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Team> Teams { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMapping());
        modelBuilder.ApplyConfiguration(new PlayerMapping());
        modelBuilder.ApplyConfiguration(new FieldPositionMapping());
        modelBuilder.ApplyConfiguration(new AddressMapping());
        modelBuilder.ApplyConfiguration(new TeamMapping());

        base.OnModelCreating(modelBuilder);
    }

}
