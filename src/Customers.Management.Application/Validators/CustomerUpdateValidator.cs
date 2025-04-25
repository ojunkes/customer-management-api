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
            .Length(2, 60)
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.TaxId)
            .Matches(@"^\d{11}$")
            .When(x => !string.IsNullOrWhiteSpace(x.TaxId));

        RuleFor(x => x.Address)
            .Length(2, 100)
            .When(x => !string.IsNullOrWhiteSpace(x.Address));

        RuleFor(x => x.City)
            .Length(2, 40)
            .When(x => !string.IsNullOrWhiteSpace(x.City));

        RuleFor(x => x.ZipCode)
            .Matches(@"^\d{8}$")
            .When(x => !string.IsNullOrWhiteSpace(x.ZipCode));

        RuleFor(x => x.State)
            .Length(2, 40)
            .When(x => !string.IsNullOrWhiteSpace(x.State));

        RuleFor(x => x.Country)
            .Length(2, 30)
            .When(x => !string.IsNullOrWhiteSpace(x.Country));
    }
}