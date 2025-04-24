using System.ComponentModel;

namespace Customers.Management.Domain.Enums
{
    public enum StatusCustomer
    {
        [Description("Inativo")]
        Inactive = 0,
        [Description("Ativo")]
        Active = 1
    }
}
