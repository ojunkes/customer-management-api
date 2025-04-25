using Customers.Management.Application.Commons;
using Customers.Management.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Customers.Management.Application.Tests.Commons;

public class EnumExtensionsTests
{
    [Fact]
    public void GetDescription_ShouldReturnDescriptionAttribute_WhenPresent()
    {
        var value = SignupChannel.Website;

        var description = value.GetDescription();

        description.Should().Be("Website");
    }

    [Fact]
    public void GetDescription_ShouldReturnCorrectDescriptions_ForAllWithAttributes()
    {
        SignupChannel.Website.GetDescription().Should().Be("Website");
        SignupChannel.MobileApp.GetDescription().Should().Be("App Mobile");
        SignupChannel.Partner.GetDescription().Should().Be("Parceiro");
        SignupChannel.Store.GetDescription().Should().Be("Loja Física");
    }
}

