using Customers.Management.Application.Commons;
using FluentAssertions;
using Xunit;

namespace Customers.Management.Application.Tests.Commons;

public class DomainExceptionTests
{
    [Fact]
    public void Constructor_ShouldSetMessage()
    {
        var expectedMessage = "Erro de domínio";

        var exception = new DomainException(expectedMessage);

        exception.Should().BeOfType<DomainException>();
        exception.Message.Should().Be(expectedMessage);
    }

    [Fact]
    public void DomainException_ShouldInheritFromException()
    {
        var exception = new DomainException("Mensagem");

        exception.Should().BeAssignableTo<Exception>();
    }
}

