﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TBase> builder)
    {
        builder.HasKey(b => b.Id).IsClustered();
        builder.Property(b => b.Id)
               .UsePropertyAccessMode(PropertyAccessMode.FieldDuringConstruction)
               .IsRequired(true);

        builder.Property(b => b.CreatedAt)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired(true);

        builder.Property(b => b.ModifiedAt)
               .UsePropertyAccessMode(PropertyAccessMode.Field)
               .IsRequired(false);
    }
}
