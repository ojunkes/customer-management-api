using Customers.Management.Domain.Core;

namespace Customers.Management.Domain.Entities;

public class Address : BaseEntity
{
    public string ZipCode { get; private set; } = null!;
    public string? Street { get; private set; }
    public string? Complement { get; private set; }
    public string? Unit { get; private set; }
    public string? Neighborhood { get; private set; }
    public string? City { get; private set; }
    public string? StateInitials { get; private set; }
    public string? State { get; private set; }
    public string? Region { get; private set; }
    public string? IbgeCode { get; private set; }
    public string? Gia { get; private set; }
    public string? AreaCode { get; private set; }
    public string? SiafiCode { get; private set; }

    public Address(
        string zipCode,
        string street,
        string complement,
        string unit,
        string neighborhood,
        string city,
        string stateInitials,
        string state,
        string region,
        string ibgeCode,
        string gia,
        string areaCode,
        string siafiCode)
    {
        ZipCode = zipCode;
        Street = street;
        Complement = complement;
        Unit = unit;
        Neighborhood = neighborhood;
        City = city;
        StateInitials = stateInitials;
        State = state;
        Region = region;
        IbgeCode = ibgeCode;
        Gia = gia;
        AreaCode = areaCode;
        SiafiCode = siafiCode;
    }

    public void Update(
        string zipCode,
        string street,
        string complement,
        string unit,
        string neighborhood,
        string city,
        string stateInitials,
        string state,
        string region,
        string ibgeCode,
        string gia,
        string areaCode,
        string siafiCode)
    {
        ZipCode = zipCode;
        Street = street;
        Complement = complement;
        Unit = unit;
        Neighborhood = neighborhood;
        City = city;
        StateInitials = stateInitials;
        State = state;
        Region = region;
        IbgeCode = ibgeCode;
        Gia = gia;
        AreaCode = areaCode;
        SiafiCode = siafiCode;
    }
}
