using Customers.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Management.Infra.Mappers;

internal class AddressMapping : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(c => c.ZipCode)
            .IsRequired();

        builder.HasIndex(c => c.ZipCode)
               .IsUnique();

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(c => c.ModifiedAt)
            .HasColumnType("DATETIMEOFFSET");

        builder.ToTable("Address");
    }
}
