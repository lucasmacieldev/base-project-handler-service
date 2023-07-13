using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasColumnType("varchar(100)");
        builder.Property(b => b.Email).IsRequired().HasColumnType("varchar(150)");
        builder.Property(b => b.BirthDate).IsRequired().HasColumnType("varchar(100)");
        builder.Property(b => b.Document).IsRequired().HasColumnType("varchar(20)");
        builder.Property(b => b.Password).IsRequired().HasColumnType("varchar(MAX)");
        builder.Property(b => b.Key).IsRequired();
        builder.Property(b => b.Type).IsRequired();
    }
}
