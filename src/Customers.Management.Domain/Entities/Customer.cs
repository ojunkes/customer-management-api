using Customers.Management.Domain.Core;
using Customers.Management.Domain.Enums;

namespace Customers.Management.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string TaxId { get; private set; } = null!;
    public DateOnly DateOfBirth { get; private set; }
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string ZipCode { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public SignupChannel SignupChannel { get; private set; }

    public Customer(
        Guid id,
        string name,
        string taxId,
        DateOnly dateOfBirth,
        string address,
        string city,
        string zipCode,
        string state,
        string country,
        SignupChannel signupChannel)
    {
        base.Id = id;
        Name = name;
        TaxId = taxId;
        DateOfBirth = dateOfBirth;
        Address = address;
        City = city;
        ZipCode = zipCode;
        State = state;
        Country = country;
        SignupChannel = signupChannel;
    }
}
