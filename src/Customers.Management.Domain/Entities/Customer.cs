using Customers.Management.Domain.Core;
using Customers.Management.Domain.Enums;

namespace Customers.Management.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Cpf { get; private set; } = null!;
    public DateOnly DateOfBirth { get; private set; }
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string ZipCode { get; private set; } = null!;
    public string State { get; private set; } = null!;
    public string Country { get; private set; } = null!;
    public StatusCustomer Status { get; private set; }
}
