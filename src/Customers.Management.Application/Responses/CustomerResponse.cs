using Customers.Management.Application.Commons;
using Customers.Management.Domain.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Customers.Management.Application.Responses;

[ExcludeFromCodeCoverage]
public record CustomerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string TaxId { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string ZipCode { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string SignupChannel { get; set; } = null!;
}

public static class CustomerResponseExtensions
{
    public static CustomerResponse ToResponse(this Customer customer)
    {
        return new CustomerResponse
        {
            Id = customer.Id,
            Name = customer.Name,
            TaxId = customer.TaxId,
            DateOfBirth = customer.DateOfBirth,
            Street = customer.Street,
            City = customer.City,
            ZipCode = customer.ZipCode,
            State = customer.State,
            Country = customer.Country,
            SignupChannel = customer.SignupChannel.GetDescription()
        };
    }

    public static IEnumerable<CustomerResponse> ToResponse(this IEnumerable<Customer>? customers)
    {
        return customers?.Select(c => c.ToResponse()) ?? [];
    }
}
