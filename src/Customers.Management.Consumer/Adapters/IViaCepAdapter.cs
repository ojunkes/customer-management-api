using Customers.Management.Consumer.Responses;

namespace Customers.Management.Consumer.Adapters;

public interface IViaCepAdapter
{
    Task<EnderecoResponse?> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken);
}
