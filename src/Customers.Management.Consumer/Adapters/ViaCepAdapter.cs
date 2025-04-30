using Customers.Management.Consumer.Responses;
using System.Text.Json;

namespace Customers.Management.Consumer.Adapters;

public class ViaCepAdapter : IViaCepAdapter
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "https://viacep.com.br/ws/";

    public ViaCepAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnderecoResponse?> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"{BaseUrl}{zipCode}/json/", cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<EnderecoResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        return null;
    }
}
