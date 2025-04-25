using Customers.Management.Application.Shared;
using Customers.Management.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Customers.Management.Application.Tests.Shared;

public class EnumExtensionsTests
{
    [Fact]
    public void GetDescription_ShouldReturnDescriptionAttribute_WhenPresent()
    {
        var value = StatusCustomer.Active;

        var description = value.GetDescription();

        description.Should().Be("Ativo");
    }

    [Fact]
    public void GetDescription_ShouldReturnCorrectDescriptions_ForAllWithAttributes()
    {
        StatusCustomer.Active.GetDescription().Should().Be("Ativo");
        StatusCustomer.Inactive.GetDescription().Should().Be("Inativo");
    }
}

