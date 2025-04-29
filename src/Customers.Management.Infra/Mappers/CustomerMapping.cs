using Customers.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Management.Infra.Mappers;

internal class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnType("UNIQUEIDENTIFIER");

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(c => c.TaxId)
            .IsRequired()
            .HasMaxLength(11);

        builder.HasIndex(c => c.TaxId)
            .IsUnique();

        builder.Property(c => c.DateOfBirth)
            .IsRequired();

        builder.Property(c => c.Street)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(c => c.City)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(c => c.ZipCode)
            .IsRequired()
            .HasMaxLength(8);

        builder.Property(c => c.State)
            .IsRequired()
            .HasMaxLength(40);

        builder.Property(c => c.Country)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasColumnType("DATETIMEOFFSET");

        builder.Property(c => c.ModifiedAt)
            .HasColumnType("DATETIMEOFFSET");

        builder.ToTable("Customer");
    }
}
