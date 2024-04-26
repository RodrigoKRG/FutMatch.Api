using FutMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutMatch.Infra.Data.Mappings;

public class PlayerMapping : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasColumnName("Name").HasColumnType("varchar(50)").IsRequired();
        builder.Property(p => p.Cpf).HasColumnName("Cpf").HasColumnType("varchar(15)").IsRequired();
        builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");
        builder.Property(p => p.Email).HasColumnName("Email").HasColumnType("varchar(50)").IsRequired();
        builder.Property(p => p.Phone).HasColumnName("Phone").HasColumnType("varchar(15)").IsRequired();
        builder.Property(p => p.AddressId).HasColumnName("AddressId");
        builder.Property(p => p.UserId).HasColumnName("UserId").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("CreatedAt").IsRequired();
        builder.Property(p => p.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(50)").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("UpdatedAt");
        builder.Property(p => p.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(50)");
        builder.HasMany(p => p.Positions).WithMany(p => p.Players).UsingEntity(j => j.ToTable("PlayerFieldPosition"));
        builder.HasMany(p => p.Teams).WithMany(p => p.Players).UsingEntity(j => j.ToTable("PlayerTeam"));
        builder.HasOne(p => p.Address).WithMany().HasForeignKey(p => p.AddressId);
        builder.HasOne(p => p.User).WithMany().HasForeignKey(p => p.UserId);
    }
}
