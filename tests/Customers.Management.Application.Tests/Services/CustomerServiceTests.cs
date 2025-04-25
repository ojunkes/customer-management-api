using AutoMapper;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Services;
using Customers.Management.Application.Shared;
using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Enums;
using Customers.Management.Infra.Repositories;
using FluentAssertions;
using Moq;
using Xunit;

namespace Customers.Management.Application.Tests.Services;

public class CustomerServiceTests
{
    private readonly Mock<ICustomerRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ICustomerService _customerService;

    public CustomerServiceTests()
    {
        _repositoryMock = new Mock<ICustomerRepository>();
        _mapperMock = new Mock<IMapper>();

        _customerService = new CustomerService(_repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllCustomersAsync_ShouldReturnCustomers()
    {
        var customers = new List<Customer>()
        {
            new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active),
            new Customer(Guid.NewGuid(), "Nome 2", "12345678902", DateOnly.FromDateTime(DateTime.Now), "Rua 2", "Cidade 2", "12345-123", "Estado 2", "País 2", StatusCustomer.Inactive)
        };

        var customersResponse = new List<CustomerResponse>
        {
            new CustomerResponse()
            {
                Id = customers[0].Id,
                Name = customers[0].Name,
                Cpf = customers[0].Cpf,
                DateOfBirth = customers[0].DateOfBirth,
                Address = customers[0].Address,
                City = customers[0].City,
                ZipCode = customers[0].ZipCode,
                State = customers[0].State,
                Country = customers[0].Country,
                Status  = customers[0].Status.GetDescription()
            },
            new CustomerResponse()
            {
                Id = customers[1].Id,
                Name = customers[1].Name,
                Cpf = customers[1].Cpf,
                DateOfBirth = customers[1].DateOfBirth,
                Address = customers[1].Address,
                City = customers[1].City,
                ZipCode = customers[1].ZipCode,
                State = customers[1].State,
                Country = customers[1].Country,
                Status  = customers[1].Status.GetDescription()
            }
        };

        _repositoryMock.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(customers);

        _mapperMock.Setup(m => m.Map<IEnumerable<CustomerResponse>>(customers))
            .Returns(customersResponse);

        var result = await _customerService.GetAllCustomersAsync(new CancellationToken());

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Errors.Should().BeNull();
        result.Data.Should().SatisfyRespectively(
            first =>
            {
                first.Name.Should().Be("Nome 1");
                first.Cpf.Should().Be("12345678901");
                first.Status.Should().Be(StatusCustomer.Active.GetDescription());
            },
            second =>
            {
                second.Name.Should().Be("Nome 2");
                second.Cpf.Should().Be("12345678902");
                second.Status.Should().Be(StatusCustomer.Inactive.GetDescription());
            });

    }

    [Fact]
    public async Task GetCustomerByIdAsync_ShouldReturnCustomer()
    {
        var customer = new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active);

        var customerResponse = new CustomerResponse()
        {
            Id = customer.Id,
            Name = customer.Name,
            Cpf = customer.Cpf,
            DateOfBirth = customer.DateOfBirth,
            Address = customer.Address,
            City = customer.City,
            ZipCode = customer.ZipCode,
            State = customer.State,
            Country = customer.Country,
            Status = customer.Status.GetDescription()
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _mapperMock.Setup(m => m.Map<CustomerResponse>(customer))
            .Returns(customerResponse);

        var result = await _customerService.GetCustomerAsync(customer.Id, new CancellationToken());

        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Errors.Should().BeNull();
        result.Data!.Name.Should().Be(customer.Name);
        result.Data.Cpf.Should().Be(customer.Cpf);
        result.Data.Status.Should().Be(StatusCustomer.Active.GetDescription());
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
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345-123",
            State = "Estado 1",
            Country = "País 1",
            Status = StatusCustomer.Active
        };

        var customer = new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active);

        var customerResponse = new CustomerResponse()
        {
            Id = customer.Id,
            Name = customer.Name,
            Cpf = customer.Cpf,
            DateOfBirth = customer.DateOfBirth,
            Address = customer.Address,
            City = customer.City,
            ZipCode = customer.ZipCode,
            State = customer.State,
            Country = customer.Country,
            Status = customer.Status.GetDescription()
        };

        _mapperMock.Setup(m => m.Map<Customer>(customerRequest))
            .Returns(customer);

        _mapperMock.Setup(m => m.Map<CustomerResponse>(customer))
            .Returns(customerResponse);

        var result = await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Once());
        result.Success.Should().BeTrue();
        result.Errors.Should().BeNull();
        result.Data!.Name.Should().Be(customer.Name);
        result.Data.Cpf.Should().Be(customer.Cpf);
        result.Data.Status.Should().Be(StatusCustomer.Active.GetDescription());
    }

    [Fact]
    public async Task InsertCustomerAsync_ShouldNotInsertCustomer_WhenIdExistInBodyRequest()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345-123",
            State = "Estado 1",
            Country = "País 1",
            Status = StatusCustomer.Active
        };

        Func<Task> act = async () => await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("Id informado no corpo da requisição.");

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
    }

    [Fact]
    public async Task InsertCustomerAsync_ShouldNotInsertCustomer_WhenCpfExistInDatabase()
    {
        var customerRequest = new CustomerRequest()
        {
            Name = "Nome 1",
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345-123",
            State = "Estado 1",
            Country = "País 1",
            Status = StatusCustomer.Active
        };

        var customer = new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active);

        _repositoryMock.Setup(r => r.GetByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        Func<Task> act = async () => await _customerService.InsertCustomerAsync(customerRequest, new CancellationToken());

        await act.Should()
            .ThrowAsync<DomainException>()
            .WithMessage("CPF já consta na base de dados.");

        _repositoryMock.Verify(r => r.InsertAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Never());
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldUpdateCustomer()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345-123",
            State = "Estado 1",
            Country = "País 1",
            Status = StatusCustomer.Active
        };

        var customer = new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active);

        var customerResponse = new CustomerResponse()
        {
            Id = customer.Id,
            Name = customer.Name,
            Cpf = customer.Cpf,
            DateOfBirth = customer.DateOfBirth,
            Address = customer.Address,
            City = customer.City,
            ZipCode = customer.ZipCode,
            State = customer.State,
            Country = customer.Country,
            Status = customer.Status.GetDescription()
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(customerRequest.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _mapperMock.Setup(m => m.Map(customerRequest, customer))
            .Returns(customer);

        _mapperMock.Setup(m => m.Map<CustomerResponse>(customer))
            .Returns(customerResponse);

        var result = await _customerService.UpdateCustomerAsync(customerRequest, new CancellationToken());

        _repositoryMock.Verify(r => r.Update(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Once());
        _repositoryMock.Verify(r => r.CommitAsync(), Times.Once());
        result.Success.Should().BeTrue();
        result.Errors.Should().BeNull();
        result.Data!.Name.Should().Be(customer.Name);
        result.Data.Cpf.Should().Be(customer.Cpf);
        result.Data.Status.Should().Be(StatusCustomer.Active.GetDescription());
    }

    [Fact]
    public async Task UpdateCustomerAsync_ShouldNotUpdateCustomer_WhenCustomerNotFound()
    {
        var customerRequest = new CustomerRequest()
        {
            Id = Guid.NewGuid(),
            Name = "Nome 1",
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade 1",
            ZipCode = "12345-123",
            State = "Estado 1",
            Country = "País 1",
            Status = StatusCustomer.Active
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
        var customer = new Customer(Guid.NewGuid(), "Nome 1", "12345678901", DateOnly.FromDateTime(DateTime.Now), "Rua 1", "Cidade 1", "12345-123", "Estado 1", "País 1", StatusCustomer.Active);

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
