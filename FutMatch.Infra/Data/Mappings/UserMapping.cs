using FutMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutMatch.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Login).HasColumnName("Login").HasColumnType("varchar(50)").IsRequired();
            builder.Property(u => u.Password).HasColumnName("Password").HasColumnType("varchar(50)").IsRequired();
            builder.Property(u => u.Salt).HasColumnName("Salt").HasColumnType("varbinary(16)").IsRequired();
            builder.Property(u => u.Role).HasColumnName("Role").IsRequired();
            builder.Property(u => u.Active).HasColumnName("Active").IsRequired();
            builder.Property(u => u.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.Property(u => u.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(50)").IsRequired();
            builder.Property(u => u.UpdatedAt).HasColumnName("UpdatedAt");
            builder.Property(u => u.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(50)");
        }
    }
}
