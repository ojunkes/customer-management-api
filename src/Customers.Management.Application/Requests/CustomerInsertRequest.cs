using Customers.Management.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Management.Application.Requests;

public record CustomerInsertRequest
{    
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo Nome é obrigatório.")]
    [StringLength(60, MinimumLength = 2)]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "O campo CPF é obrigatório.")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
    public string Cpf { get; set; } = null!;

    [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "O campo Endereço é obrigatório.")]
    [StringLength(100, MinimumLength = 2)]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
    [StringLength(40, MinimumLength = 2)]
    public string City { get; set; } = null!;

    [Required(ErrorMessage = "O campo CEP é obrigatório.")]
    [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "Formato inválido de CEP.")]
    public string ZipCode { get; set; } = null!;

    [Required(ErrorMessage = "O campo Estado é obrigatório.")]
    [StringLength(40, MinimumLength = 2)]
    public string State { get; set; } = null!;

    [Required(ErrorMessage = "O campo País é obrigatório.")]
    [StringLength(30, MinimumLength = 2)]
    public string Country { get; set; } = null!;

    [Required(ErrorMessage = "O campo Status é obrigatório.")]
    [EnumDataType(typeof(StatusCustomer))]
    public StatusCustomer Status { get; set; }
}
