using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Management.Infra.Mappers
{
    internal class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnType("UNIQUEIDENTIFIER");

            builder.Property(c => c.Code)
                .IsRequired();

            builder.HasIndex(c => c.Code)
                .IsUnique();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(c => c.Cpf)
                .IsUnique();

            builder.Property(c => c.DateOfBirth)
                .IsRequired();

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.City)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(c => c.ZipCode)
                .IsRequired()
                .HasMaxLength(15);

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
}
