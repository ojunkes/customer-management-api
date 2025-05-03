using Customers.Management.Application.Requests;
using FluentValidation;

namespace Customers.Management.Application.Validators;

public class CustomerInsertValidator : AbstractValidator<CustomerRequest>
{
    public CustomerInsertValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .Length(2, 60).WithMessage("O campo Nome deve ter entre 2 e 60 caracteres.");

        RuleFor(x => x.TaxId)
            .NotEmpty().WithMessage("O campo CPF é obrigatório.")
            .Matches(@"^\d{11}$").WithMessage("CPF deve conter 11 dígitos.");

        RuleFor(x => x.DateOfBirth)
            .NotNull().WithMessage("O campo Data de Nascimento é obrigatório.");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("O campo Rua é obrigatório.")
            .Length(2, 100).WithMessage("O campo Rua deve ter entre 2 e 100 caracteres.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("O campo Cidade é obrigatório.")
            .Length(2, 40).WithMessage("O campo Cidade deve ter entre 2 e 40 caracteres.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O campo CEP é obrigatório.")
            .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos numéricos.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("O campo Estado é obrigatório.")
            .Length(2, 40).WithMessage("O campo Estado deve ter entre 2 e 40 caracteres.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("O campo País é obrigatório.")
            .Length(2, 30).WithMessage("O campo País deve ter entre 2 e 30 caracteres.");

        RuleFor(x => x.SignupChannel)
            .NotNull().WithMessage("O campo Canal de Inscrição é obrigatório.");
    }
}

