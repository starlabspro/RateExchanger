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
    private const string RateExchangeApi = "/api/v1/RateExchanger";

    public RateExchangerService(IOptions<RateExchangerOptions> rateExchangerOptions)
    {
        _client = new RestClient(rateExchangerOptions.Value.BaseUrl)
            .AddDefaultHeader("apikey", rateExchangerOptions.Value.ApiKey);
    }

    /// <inheritdoc />
    public async Task<GetExchangeRateResponse> GetExchangeRateAsync(GetExchangeRateRequest request)
    {
        var restRequest = new RestRequest(RateExchangeApi, Method.Post).AddJsonBody(request);
        return await _client.PostAsync<GetExchangeRateResponse>(restRequest);
    }
}

/// <summary>
/// The Rate Exchanger Options.
/// </summary>
public class RateExchangerOptions
{
    /// <summary>
    /// The Base Url for the Rate Exchanger API.
    /// </summary>
    public string BaseUrl { get; set; }
    /// <summary>
    /// The API Key for the Rate Exchanger API.
    /// </summary>
    public string ApiKey { get; set; }
}

public class GetExchangeRateRequest
{
    public string BaseCurrency { get; set; } 
    public string[] OtherCurrencies { get; set; }
};

public record GetExchangeRateResponse(string BaseCurrency, Dictionary<string, decimal> Rates);