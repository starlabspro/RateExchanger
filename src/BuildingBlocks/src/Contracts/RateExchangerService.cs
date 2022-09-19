using Microsoft.Extensions.Options;
using RestSharp;

namespace BuildingBlocks.Contracts;

public interface IRateExchangerService
{
    /// <summary>
    /// Gets the Exchange Rate for the given currency.
    /// </summary>
    /// <param name="request">The <see cref="GetExchangeRateRequest"/>.</param>
    /// <returns>The <see cref="GetExchangeRateResponse"/>.</returns>
    public Task<GetExchangeRateResponse> GetExchangeRateAsync(GetExchangeRateRequest request);
}

public class RateExchangerService : IRateExchangerService
{
    private readonly RestClient _client;

    public RateExchangerService(IOptions<RateExchangerOptions> rateExchangerOptions)
    {
        _client = new RestClient(rateExchangerOptions.Value.BaseUrl)
            .AddDefaultHeader("apikey", rateExchangerOptions.Value.ApiKey);
    }

    public async Task<GetExchangeRateResponse> GetExchangeRateAsync(GetExchangeRateRequest request)
    {
        return await _client.GetJsonAsync<GetExchangeRateResponse>(request.BaseCurrency, request.OtherSymbols);
    }
}

public record RateExchangerOptions(string BaseUrl, string ApiKey);

public record GetExchangeRateRequest(string BaseCurrency, string[] OtherSymbols);

public record GetExchangeRateResponse(string BaseCurrency, Dictionary<string, decimal> Rates);