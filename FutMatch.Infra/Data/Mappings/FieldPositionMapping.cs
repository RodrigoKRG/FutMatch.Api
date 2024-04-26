using FutMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FutMatch.Infra.Data.Mappings
{
    public class FieldPositionMapping : IEntityTypeConfiguration<FieldPosition>
    {
        public void Configure(EntityTypeBuilder<FieldPosition> builder)
        {
            builder.HasKey(fp => fp.Id);
            builder.Property(fp => fp.Name).HasColumnName("Name").HasColumnType("varchar(50)").IsRequired();
            builder.HasMany(fp => fp.Players).WithMany(p => p.Positions).UsingEntity(j => j.ToTable("PlayerFieldPosition"));
        }
    }
}
