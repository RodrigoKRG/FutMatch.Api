using FutMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutMatch.Infra.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Street).HasColumnName("Street").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.Number).HasColumnName("Number").HasColumnType("varchar(10)").IsRequired();
            builder.Property(a => a.Complement).HasColumnName("Complement").HasColumnType("varchar(50)");
            builder.Property(a => a.Neighborhood).HasColumnName("Neighborhood").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.City).HasColumnName("City").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.State).HasColumnName("State").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.Country).HasColumnName("Country").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.ZipCode).HasColumnName("ZipCode").HasColumnType("varchar(10)").IsRequired();
            builder.Property(a => a.Latitude).HasColumnName("Latitude").IsRequired();
            builder.Property(a => a.Longitude).HasColumnName("Longitude").IsRequired();
            builder.Property(a => a.CreatedAt).HasColumnName("CreatedAt").IsRequired();
            builder.Property(a => a.CreatedBy).HasColumnName("CreatedBy").HasColumnType("varchar(50)").IsRequired();
            builder.Property(a => a.UpdatedAt).HasColumnName("UpdatedAt");
            builder.Property(a => a.UpdatedBy).HasColumnName("UpdatedBy").HasColumnType("varchar(50)");
        }
    }
}
