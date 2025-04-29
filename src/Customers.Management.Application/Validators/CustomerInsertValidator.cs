using Customers.Management.Application.Requests;
using FluentValidation;

namespace Customers.Management.Application.Validators;

public class CustomerInsertValidator : AbstractValidator<CustomerRequest>
{
    public CustomerInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .Length(2, 60);

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("O campo CPF é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos.");

        RuleFor(x => x.DateOfBirth)
            .NotNull().WithMessage("O campo Data de Nascimento é obrigatório.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("O campo Endereço é obrigatório.")
            .Length(2, 100);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("O campo Cidade é obrigatório.")
            .Length(2, 40);

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O campo CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("O campo Estado é obrigatório.")
            .Length(2, 40);

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("O campo País é obrigatório.")
            .Length(2, 30);

        RuleFor(x => x.SignupChannel)
            .NotNull().WithMessage("O campo Canal de Inscrição é obrigatório.");
    }
}

