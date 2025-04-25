using Customers.Management.Application.Requests;
using Customers.Management.Application.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace Customers.Management.Application.Tests.Validators;

public class CustomerUpdateValidatorTests
{
    private readonly CustomerUpdateValidator _validator;

    public CustomerUpdateValidatorTests()
    {
        _validator = new CustomerUpdateValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Id_Is_Empty()
    {
        var model = new CustomerRequest { Id = Guid.Empty };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Id)
              .WithErrorMessage("O campo Id é obrigatório.");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_TooShort()
    {
        var model = new CustomerRequest { Name = "A" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Name_Is_TooLong()
    {
        var model = new CustomerRequest { Name = new string('A', 61) };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_TaxId_Is_Invalid()
    {
        var model = new CustomerRequest { TaxId = "123456789" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.TaxId);
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_TooShort()
    {
        var model = new CustomerRequest { Address = "A" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Address);
    }

    [Fact]
    public void Should_Have_Error_When_Address_Is_TooLong()
    {
        var model = new CustomerRequest { Address = new string('A', 101) };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Address);
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_TooShort()
    {
        var model = new CustomerRequest { City = "A" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_TooLong()
    {
        var model = new CustomerRequest { City = new string('A', 41) };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_ZipCode_Is_Invalid()
    {
        var model = new CustomerRequest { ZipCode = "12345" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.ZipCode);
    }

    [Fact]
    public void Should_Have_Error_When_State_Is_TooShort()
    {
        var model = new CustomerRequest { State = "A" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Fact]
    public void Should_Have_Error_When_State_Is_TooLong()
    {
        var model = new CustomerRequest { State = new string('A', 41) };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Fact]
    public void Should_Have_Error_When_Country_Is_TooShort()
    {
        var model = new CustomerRequest { Country = "A" };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Country);
    }

    [Fact]
    public void Should_Have_Error_When_Country_Is_TooLong()
    {
        var model = new CustomerRequest { Country = new string('A', 31) };
        var result = _validator.TestValidate(model);

        result.ShouldHaveValidationErrorFor(x => x.Country);
    }

    [Fact]
    public void Should_Not_Have_Any_Errors_When_Fields_Are_Valid()
    {
        var model = new CustomerRequest
        {
            Id = Guid.NewGuid(),
            Name = "Maria",
            TaxId = "12345678901",
            Address = "Rua Central, 123",
            City = "Curitiba",
            ZipCode = "12345678",
            State = "PR",
            Country = "Brasil"
        };

        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}

