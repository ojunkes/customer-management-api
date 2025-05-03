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
    public void Should_Validate_TaxId_ErrorMessages_When_Empty()
    {
        var model = new CustomerRequest { TaxId = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.TaxId)
              .WithErrorMessage("O campo CPF é obrigatório.");
    }

    [Fact]
    public void Should_Validate_TaxId_ErrorMessages_When_Invalid()
    {
        var model = new CustomerRequest { TaxId = "1234" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.TaxId)
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
    public void Should_Validate_Street_ErrorMessage()
    {
        var model = new CustomerRequest { Street = "" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Street)
              .WithErrorMessage("O campo Rua é obrigatório.");
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
              .WithErrorMessage("CEP deve conter 8 dígitos numéricos.");
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
    public void Should_Validate_SignupChannel_ErrorMessage()
    {
        var model = new CustomerRequest { SignupChannel = null };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.SignupChannel)
              .WithErrorMessage("O campo Canal de Inscrição é obrigatório.");
    }

    [Fact]
    public void Should_Not_Have_Any_Errors_When_Model_Is_Valid()
    {
        var model = new CustomerRequest
        {
            Name = "Anderson",
            TaxId = "12345678901",
            DateOfBirth = new DateOnly(1995, 5, 20),
            Street = "Rua dos Testes",
            City = "São Paulo",
            ZipCode = "12345678",
            State = "SP",
            Country = "Brasil",
            SignupChannel = SignupChannel.Website
        };

        var result = _validator.TestValidate(model);
        result.IsValid.Should().BeTrue();
    }
}
