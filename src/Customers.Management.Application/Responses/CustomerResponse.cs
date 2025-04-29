using System.Diagnostics.CodeAnalysis;

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
