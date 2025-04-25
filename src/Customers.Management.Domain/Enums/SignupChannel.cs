using System.ComponentModel;

namespace Customers.Management.Domain.Enums;

public enum SignupChannel
{
    [Description("Website")]
    Website,

    [Description("App Mobile")]
    MobileApp,

    [Description("Parceiro")]
    Partner,

    [Description("Loja Física")]
    Store
}
