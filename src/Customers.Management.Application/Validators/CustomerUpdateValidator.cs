using Customers.Management.Application.Requests;
using FluentValidation;

namespace Customers.Management.Application.Validators;

public class CustomerUpdateValidator : AbstractValidator<CustomerRequest>
{
    public CustomerUpdateValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O campo Id é obrigatório.");

        RuleFor(x => x.Name)
            .Length(2, 60).WithMessage("O campo Nome deve ter entre 2 e 60 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.TaxId)
            .Matches(@"^\d{11}$")
            .When(x => !string.IsNullOrWhiteSpace(x.TaxId));

        RuleFor(x => x.Street)
            .Length(2, 100).WithMessage("O campo Rua deve ter entre 2 e 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Street));

        RuleFor(x => x.City)
            .Length(2, 40).WithMessage("O campo Cidade deve ter entre 2 e 40 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.ZipCode)
            .Matches(@"^\d{8}$").WithMessage("CEP deve conter 8 dígitos numéricos.")
            .When(x => !string.IsNullOrWhiteSpace(x.ZipCode));

        RuleFor(x => x.State)
            .Length(2, 40).WithMessage("O campo Estado deve ter entre 2 e 40 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.State));

        RuleFor(x => x.Country)
            .Length(2, 30).WithMessage("O campo País deve ter entre 2 e 30 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Country));
    }
}