using Customers.Management.Domain.Interfaces.Adapters;
using Customers.Management.Domain.Responses;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net.Http.Json;
using System.Text.Json;

namespace Customers.Management.Infra.Adapters;

public class ViaCepAdapter : IViaCepAdapter
{
    private readonly ILogger<ViaCepAdapter> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;

    public ViaCepAdapter(
        ILogger<ViaCepAdapter> logger,
        IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;

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
        var httpClient = _httpClientFactory.CreateClient("ViaCep");
        var response = await _retryPolicy.ExecuteAsync(() =>
                            httpClient.GetAsync($"{zipCode}/json/", cancellationToken));

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<EnderecoResponse>();
    }
}
