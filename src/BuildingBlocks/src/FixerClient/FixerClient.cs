using BuildingBlocks.FixerClient.Contracts;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BuildingBlocks.FixerClient;

/// <inheritdoc />
public class FixerClient : IFixerClient
{
    private readonly RestClient _client;

    public FixerClient(IOptions<FixerOptions> fixerOptions)
    {
        _client = new RestClient(fixerOptions.Value.BaseUri);
        _client.AddDefaultHeader("apikey", fixerOptions.Value.ApiKey);
    }

    /// <inheritdoc />
    public async Task<GetLatestRatesResponse> GetLatestAsync(string baseCurrency, params string[] symbols)
    {
        return await _client.GetJsonAsync<GetLatestRatesResponse>(
            $"latest?symbols={string.Join(",", symbols)}&base={baseCurrency}");
    }
}