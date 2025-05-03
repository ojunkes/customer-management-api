using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Enums;
using MassTransit.SqlTransport.Topology;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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

public static class CustomerRequestExtensions
{
    public static Customer ToEntity(this CustomerRequest request)
    {
        return new Customer(
            request.Name!,
            request.TaxId!,
            request.DateOfBirth!.Value,
            request.Street!,
            request.City!,
            request.ZipCode!,
            request.State!,
            request.Country!,
            request.SignupChannel!.Value
        );
    }

    public static void UpdateFrom(this Customer customer, CustomerRequest request)
    {
        customer.Update(
            string.IsNullOrWhiteSpace(request.Name) ? customer.Name : request.Name,
            string.IsNullOrWhiteSpace(request.TaxId) ? customer.TaxId : request.TaxId,
            request.DateOfBirth ?? customer.DateOfBirth,
            string.IsNullOrWhiteSpace(request.Street) ? customer.Street : request.Street,
            string.IsNullOrWhiteSpace(request.City) ? customer.City : request.City,
            string.IsNullOrWhiteSpace(request.ZipCode) ? customer.ZipCode : request.ZipCode,
            string.IsNullOrWhiteSpace(request.State) ? customer.State : request.State,
            string.IsNullOrWhiteSpace(request.Country) ? customer.Country : request.Country,
            request.SignupChannel ?? customer.SignupChannel);
    }
}