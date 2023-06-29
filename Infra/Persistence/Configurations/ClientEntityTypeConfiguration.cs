using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class ClientEntityTypeConfiguration : BaseEntityTypeConfiguration<Client>
{
    public override void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(nameof(Client));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasColumnType("varchar(100)");
        builder.Property(b => b.ZipCode).IsRequired().HasColumnType("varchar(100)");
        builder.Property(b => b.Street).IsRequired().HasColumnType("varchar(255)");
        builder.Property(b => b.Type).IsRequired();
    }
}
