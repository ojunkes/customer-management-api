using Customers.Management.Domain.Interfaces.Adapters;
using Customers.Management.Domain.Responses;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Text.Json;

namespace Customers.Management.Infra.Adapters;

public class ViaCepAdapter : IViaCepAdapter
{
    private readonly ILogger<ViaCepAdapter> _logger;
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public ViaCepAdapter(
        ILogger<ViaCepAdapter> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://viacep.com.br/ws/");

        var retryDelays = new[]
        {
            TimeSpan.FromSeconds(2),
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(10)
        };

        _retryPolicy = Policy<HttpResponseMessage>
            .HandleResult(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: retryDelays.Length,
                sleepDurationProvider: retryAttempt => retryDelays[retryAttempt - 1],
                onRetry: (outcome, timeSpan, retryCount, context) =>
                {
                    _logger.LogError("Tentativa {retryCount} após {timeSpan} segudos devido a: {result}", retryCount, timeSpan.TotalSeconds, outcome.Result);
                });
    }

    public async Task<EnderecoResponse?> GetAddressByZipCodeAsync(string zipCode, CancellationToken cancellationToken)
    {
        var response = await _retryPolicy.ExecuteAsync(() =>
            _httpClient.GetAsync($"{zipCode}/json/", cancellationToken));

        if (!response.IsSuccessStatusCode)
            return null;

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<EnderecoResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}
