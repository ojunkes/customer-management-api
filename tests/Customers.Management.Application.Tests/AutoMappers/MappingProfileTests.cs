using AutoMapper;
using Customers.Management.Application.AutoMappers;
using Customers.Management.Application.Requests;
using Customers.Management.Application.Responses;
using Customers.Management.Application.Shared;
using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Enums;
using FluentAssertions;
using Xunit;

namespace Customers.Management.Application.Tests.AutoMappers;

public class MappingProfileTests
{
    private readonly IMapper _mapper;

    public MappingProfileTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
    }

    [Fact]
    public void CreateMap_ShouldMapCustomerToCustomerResponseWithStatusDescription()
    {
        var customer = new Customer(Guid.NewGuid(), 
                                    "Nome 1", 
                                    "12345678901", 
                                    DateOnly.FromDateTime(DateTime.Now), 
                                    "Rua 1", 
                                    "Cidade 1", 
                                    "12345-123", 
                                    "Estado 1", 
                                    "País 1", 
                                    StatusCustomer.Active);

        var result = _mapper.Map<CustomerResponse>(customer);

        result.Should().NotBeNull();
        result.Name.Should().Be(customer.Name);
        result.Cpf.Should().Be(customer.Cpf);
        result.DateOfBirth.Should().Be(customer.DateOfBirth);
        result.Address.Should().Be(customer.Address);
        result.City.Should().Be(customer.City);
        result.ZipCode.Should().Be(customer.ZipCode);
        result.State.Should().Be(customer.State);
        result.Country.Should().Be(customer.Country);
        result.Status.Should().Be(StatusCustomer.Active.GetDescription()); 
    }

    [Fact]
    public void CreateMap_ShouldMapCustomerRequestToCustomerWithAllFields()
    {
        var request = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            Name = "Maria",
            Cpf = "12345678901",
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
            Address = "Rua 1",
            City = "Cidade",
            State = "Estado",
            Country = "Brasil",
            ZipCode = "12345-678",
            Status = StatusCustomer.Inactive
        };

        var result = _mapper.Map<Customer>(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Cpf.Should().Be(request.Cpf);
        result.DateOfBirth.Should().Be(request.DateOfBirth);
        result.Address.Should().Be(request.Address);
        result.City.Should().Be(request.City);
        result.ZipCode.Should().Be(request.ZipCode);
        result.State.Should().Be(request.State);
        result.Country.Should().Be(request.Country);
        result.Status.Should().Be(StatusCustomer.Inactive);
    }

    [Fact]
    public void CreateMap_ShouldNotMapNullFields()
    {
        var request = new CustomerRequest
        {
            Name = null,
            DateOfBirth = null,
            Status = null
        };

        var result = _mapper.Map<Customer>(request);

        result.Name.Should().BeNull();
        result.DateOfBirth.Should().Be(default);
        result.Status.Should().Be(default);
    }
}
