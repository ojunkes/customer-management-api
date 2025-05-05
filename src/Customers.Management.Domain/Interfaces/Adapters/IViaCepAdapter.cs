using Customers.Management.Domain.Responses;

namespace Customers.Management.Domain.Interfaces.Adapters;

public interface IViaCepAdapter
{
    Task<EnderecoResponse?> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken);
}
