using Customers.Management.Application.Requests;
using Customers.Management.Application.Validators;
using Customers.Management.Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;

namespace Customers.Management.Application.Tests.Validators;

public class CustomerInsertValidatorMessagesTests
{
    private readonly CustomerInsertValidator _validator;

    public CustomerInsertValidatorMessagesTests()
    {
        _validator = new CustomerInsertValidator();
    }

    [Fact]
    public void Should_Validate_Name_ErrorMessages()
    {
        var model = new CustomerRequest { Name = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("O campo Nome é obrigatório.");
    }

    [Fact]
    public void Should_Validate_Cpf_ErrorMessages_When_Empty()
    {
        var model = new CustomerRequest { Cpf = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Cpf)
              .WithErrorMessage("O campo CPF é obrigatório.");
    }

    [Fact]
    public void Should_Validate_Cpf_ErrorMessages_When_Invalid()
    {
        var model = new CustomerRequest { Cpf = "1234" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Cpf)
              .WithErrorMessage("CPF deve conter 11 dígitos.");
    }

    [Fact]
    public void Should_Validate_DateOfBirth_ErrorMessage()
    {
        var model = new CustomerRequest { DateOfBirth = null };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth)
              .WithErrorMessage("O campo Data de Nascimento é obrigatório.");
    }

    [Fact]
    public void Should_Validate_Address_ErrorMessage()
    {
        var model = new CustomerRequest { Address = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Address)
              .WithErrorMessage("O campo Endereço é obrigatório.");
    }

    [Fact]
    public void Should_Validate_City_ErrorMessage()
    {
        var model = new CustomerRequest { City = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.City)
              .WithErrorMessage("O campo Cidade é obrigatório.");
    }

    [Fact]
    public void Should_Validate_ZipCode_ErrorMessages_When_Empty()
    {
        var model = new CustomerRequest { ZipCode = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
              .WithErrorMessage("O campo CEP é obrigatório.");
    }

    [Fact]
    public void Should_Validate_ZipCode_ErrorMessages_When_Invalid()
    {
        var model = new CustomerRequest { ZipCode = "00000" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode)
              .WithErrorMessage("Formato inválido de CEP.");
    }

    [Fact]
    public void Should_Validate_State_ErrorMessage()
    {
        var model = new CustomerRequest { State = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.State)
              .WithErrorMessage("O campo Estado é obrigatório.");
    }

    [Fact]
    public void Should_Validate_Country_ErrorMessage()
    {
        var model = new CustomerRequest { Country = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Country)
              .WithErrorMessage("O campo País é obrigatório.");
    }

    [Fact]
    public void Should_Validate_Status_ErrorMessage()
    {
        var model = new CustomerRequest { Status = null };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Status)
              .WithErrorMessage("O campo Status é obrigatório.");
    }

    [Fact]
    public void Should_Not_Have_Any_Errors_When_Model_Is_Valid()
    {
        var model = new CustomerRequest
        {
            Name = "Anderson",
            Cpf = "12345678901",
            DateOfBirth = new DateOnly(1995, 5, 20),
            Address = "Rua dos Testes",
            City = "São Paulo",
            ZipCode = "12345-678",
            State = "SP",
            Country = "Brasil",
            Status = StatusCustomer.Active
        };

        var result = _validator.TestValidate(model);
        result.IsValid.Should().BeTrue();
    }
}
