using Customer.Management.Domain.Core;
using Customer.Management.Domain.Enums;

namespace Customer.Management.Domain.Entities;

public class Customer : BaseEntity
{
    public int Code { get; private set; }
    public string Name { get; private set; } = null!;
    public long Cpf { get; private set; }
    public DateTime DateOfBirth { get; private set; }
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public long ZipCode { get; private set; }
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public StatusCustomer Status { get; private set; }
}
