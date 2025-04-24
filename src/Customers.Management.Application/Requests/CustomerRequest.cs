using Customers.Management.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Customers.Management.Application.Requests;

public record CustomerRequest
{    
    public Guid Id { get; set; }
    public int Code { get; set; }

    [Required]
    [StringLength(60, MinimumLength = 2)]
    public string Name { get; set; } = null!;
    
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 dígitos.")]
    public string Cpf { get; set; } = null!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Address { get; set; } = null!;

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string City { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "Formato inválido de CEP.")]
    public string ZipCode { get; set; } = null!;

    [Required]
    [StringLength(40, MinimumLength = 2)]
    public string State { get; set; } = null!;

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Country { get; set; } = null!;

    [Required]
    [EnumDataType(typeof(StatusCustomer))]
    public StatusCustomer Status { get; set; }
}
