using Customers.Management.Domain.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Customers.Management.Application.Requests;

[ExcludeFromCodeCoverage]
public record CustomerRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? TaxId { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public SignupChannel? SignupChannel { get; set; }
}
