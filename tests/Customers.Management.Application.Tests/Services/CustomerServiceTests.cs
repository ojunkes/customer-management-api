using Customers.Management.Application.Commons;
using Customers.Management.Application.Interfaces;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Services;
using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Enums;
using Customers.Management.Domain.Exceptions;
using Customers.Management.Domain.Interfaces.Repositories;
using Customers.Management.Domain.Messages;
using FluentAssertions;
using MassTransit;
using Moq;
using Xunit;

namespace Customers.Management.Application.Tests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repositoryMock;
    private readonly Mock<IPublishEndpoint> _publishEndpointMock;
    private readonly ICustomerService _customerService;

    public CustomerServiceTests()
    {
        _repositoryMock = new Mock<ICustomerRepository>();
        _publishEndpointMock = new Mock<IPublishEndpoint>();

        _customerService = new CustomerService(_repositoryMock.Object, _publishEndpointMock.Object);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ShouldReturnCustomers()
    {
        var customers = new List<Customer>()
        {
            new Customer("Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", SignupChannel.Website),
            new Customer("Nome 2", "12345678902", DateOnly.FromDateTime(DateTime.Now), "Rua 2", "Cidade 2", "12345-123", "Estado 2", "País 2", SignupChannel.Partner)
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);

        var result = await _customerService.GetAllCustomersAsync(new CancellationToken());

        result.Should().NotBeNull();
        result.Should().SatisfyRespectively(
            first =>
            {
                first.Name.Should().Be("Nome 1");
                first.TaxId.Should().Be("12345678901");
                first.SignupChannel.Should().Be(SignupChannel.Website.GetDescription());
            },
            second =>
            {
                second.Name.Should().Be("Nome 2");
                second.TaxId.Should().Be("12345678902");
                second.SignupChannel.Should().Be(SignupChannel.Partner.GetDescription());
            });

    }

    [Fact]
    public async Task GetCustomerByIdAsync_ShouldReturnCustomer()
    {
        var customer = new Customer("Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", SignupChannel.Website);

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var result = await _customerService.GetCustomerAsync(customer.Id, new CancellationToken());

        result.Should().NotBeNull();
        result.Name.Should().Be(customer.Name);
        result.TaxId.Should().Be(customer.TaxId);
        result.SignupChannel.Should().Be(SignupChannel.Website.GetDescription());
    }

    [Fact]
    public async Task GetCustomerByIdAsync_ShouldReturnDomainException_WhenCustomerNotFound()
    {
        Func<Task> act = async () => await _customerService.GetCustomerAsync(Guid.NewGuid(), new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Cliente não encontrado.");
    }

    [Fact]
    public async Task InsertCustomerAsync_ShouldInsertCustomer()
    {
        var customerRequest = new CustomerRequest()
        {
            Name = "Nome 1",
            TaxId = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Street = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345123",
            State = "Estado 1",
            Country = "País 1",
            SignupChannel = SignupChannel.Website
        };

        var result = await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Once());
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<ZipCodeMessage>(), It.IsAny<CancellationToken>()), Times.Once);
        result.Name.Should().Be(customerRequest.Name);
        result.TaxId.Should().Be(customerRequest.TaxId);
        result.SignupChannel.Should().Be(SignupChannel.Website.GetDescription());
    }

    [Fact]
    public async Task InsertCustomerAsync_ShouldNotInsertCustomer_WhenIdExistInBodyRequest()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            TaxId = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Street = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345123",
            State = "Estado 1",
            Country = "País 1",
            SignupChannel = SignupChannel.Website
        };

        Func<Task> act = async () => await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Id informado no corpo da requisição.");

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<ZipCodeMessage>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task InsertCustomerAsync_ShouldNotInsertCustomer_WhenTaxIdExistInDatabase()
    {
        var customerRequest = new CustomerRequest()
        {
            Name = "Nome 1",
            TaxId = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Street = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345123",
            State = "Estado 1",
            Country = "País 1",
            SignupChannel = SignupChannel.Website
        };

        var customer = new Customer("Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", SignupChannel.Website);

        _repositoryMock.Setup(r => r.GetByTaxIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        Func<Task> act = async () => await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("CPF já consta na base de dados.");

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
        _publishEndpointMock.Verify(p => p.Publish(It.IsAny<ZipCodeMessage>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldUpdateCustomer()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            TaxId = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Street = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345123",
            State = "Estado 1",
            Country = "País 1",
            SignupChannel = SignupChannel.Website
        };

        var customer = new Customer("Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", SignupChannel.Website);

        _repositoryMock.Setup(r => r.GetByIdAsync(customerRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        var result = await _customerService.UpdateCustomerAsync(customerRequest, new CancellationToken());

        _repositoryMock.Verify(r => r.Update(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Once());
        result.Name.Should().Be(customer.Name);
        result.TaxId.Should().Be(customer.TaxId);
        result.SignupChannel.Should().Be(SignupChannel.Website.GetDescription());
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldNotUpdateCustomer_WhenCustomerNotFound()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            TaxId = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Street = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345123",
            State = "Estado 1",
            Country = "País 1",
            SignupChannel = SignupChannel.Website
        };

        Func<Task> act = async () => await _customerService.UpdateCustomerAsync(customerRequest, new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Cliente não encontrado.");

        _repositoryMock.Verify(r => r.Update(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
    }

    [Fact]
    public async Task DeleteCustomerAsync_ShouldDeleteCustomer()
    {
        var customer = new Customer("Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", SignupChannel.Website);

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        await _customerService.DeleteCustomerAsync(It.IsAny<Guid>(), new CancellationToken());

        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Once());
    }

    [Fact]
    public async Task DeleteCustomerAsync_ShouldNotDeleteCustomer_WhenCustomerNotFound()
    {
        Func<Task> act = async () => await _customerService.DeleteCustomerAsync(It.IsAny<Guid>(), new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Cliente não encontrado.");

        _repositoryMock.Verify(r => r.DeleteAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
    }
}
