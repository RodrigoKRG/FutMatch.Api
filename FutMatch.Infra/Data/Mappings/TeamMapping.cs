using FutMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutMatch.Infra.Data.Mappings;

public class TeamMapping : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("varchar(50)").IsRequired();
        builder.Property(p => p.Shield).HasColumnName("Shield");
        builder.HasMany(p => p.Players).WithMany(p => p.Teams);
    }
}
