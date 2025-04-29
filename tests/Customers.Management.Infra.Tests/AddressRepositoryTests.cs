using Customers.Management.Domain.Entities;
using Customers.Management.Infra.Repositories;
using Customers.Management.Infra.Tests.Fixtures;
using FluentAssertions;
using Xunit;

namespace Customers.Management.Infra.Tests;

public class AddressRepositoryTests : IClassFixture<FixtureServiceProvider>
{
    private static bool _mocked;
    private readonly IAddressRepository _addressRepository;

    public AddressRepositoryTests(FixtureServiceProvider fixtureServiceProvider)
    {
        _addressRepository = fixtureServiceProvider.GetService<IAddressRepository>();

        InsertMock();
    }

    [Fact]
    public async Task GetByZipCodeAsync_ShouldReturnAddress()
    {
        var address = await _addressRepository.GetByZipCodeAsync("89027401", new CancellationToken());

        address.Should().NotBeNull();
        address.Neighborhood.Should().Be("Progresso");
        address.Street.Should().Be("Rua Bruno Schreiber");
    }

    [Fact]
    public async Task InsertAsync_ShouldInsertAddress()
    {
        var newAddress = new Address(
            Guid.Parse("a1f20d7d-5c41-438d-8eb2-2a0d33443e19"),
            "49000221",
            "Rua Odilon Gonçalves da Silveira",
            string.Empty,
            string.Empty,
            "Aruana",
            "Aracaju",
            "SE",
            "Sergipe",
            "Nordeste",
            "2800308",
            "",
            "79",
            "3105"
        );
        await _addressRepository.InsertAsync(newAddress, new CancellationToken());
        await _addressRepository.CommitAsync();

        var customer = await _addressRepository.GetByZipCodeAsync("49000221", new CancellationToken());

        customer.Should().NotBeNull();
        customer.Neighborhood.Should().Be("Aruana");
        customer.Street.Should().Be("Rua Odilon Gonçalves da Silveira");
    }

    [Fact]
    public async Task Update_ShouldUpdateAddress()
    {
        var address = await _addressRepository.GetByZipCodeAsync("89032605", new CancellationToken());

        typeof(Address).GetProperty("Street")?.SetValue(address, "Rua Xpto");

        await _addressRepository.Update(address!, new CancellationToken());
        await _addressRepository.CommitAsync();

        var customerNewTaxId = await _addressRepository.GetByZipCodeAsync("89032605", new CancellationToken());

        customerNewTaxId.Should().NotBeNull();
        customerNewTaxId.Street.Should().Be("Rua Xpto");
    }

    private void InsertMock()
    {
        if (_mocked)
            return;

        var address1 = new Address(
            Guid.Parse("a57c8ca4-99c0-4f07-8e72-0656dc060c2e"),
            "89027401",
            "Rua Bruno Schreiber",
            string.Empty,
            string.Empty,
            "Progresso",
            "Blumenau",
            "SC",
            "Santa Catarina",
            "Sul",
            "4202404",
            "",
            "47",
            "8047"
        );
        var address2 = new Address(
            Guid.Parse("e28fbfdd-1c5c-4e0f-8d6e-a4f2b25725f5"),
            "89032605",
            "Rua José Bagatolli",
            string.Empty,
            string.Empty,
            "Passo Manso",
            "Blumenau",
            "SC",
            "Santa Catarina",
            "Sul",
            "4202404",
            "",
            "47",
            "8047"
        );

        _addressRepository.InsertAsync(address1, new CancellationToken()).Wait();
        _addressRepository.InsertAsync(address2, new CancellationToken()).Wait();
        _addressRepository.CommitAsync().Wait();

        _mocked = true;
    }
}
