using Customers.Management.Domain.Core;

namespace Customers.Management.Domain.Entities;

public class Address : BaseEntity
{
    public string ZipCode { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string Complement { get; set; } = null!;
    public string Unit { get; set; } = null!;
    public string Neighborhood { get; set; } = null!;
    public string City { get; set; } = null!;
    public string StateInitials { get; set; } = null!;
    public string State { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string IbgeCode { get; set; } = null!;
    public string Gia { get; set; } = null!;
    public string AreaCode { get; set; } = null!;
    public string SiafiCode { get; set; } = null!;

    public Address(
        Guid id,
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
        base.Id = id;
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
