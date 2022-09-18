using BuildingBlocks.FixerClient.Contracts;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BuildingBlocks.FixerClient
{
    /// <inheritdoc />
    public class FixerClient : IFixerClient
    {
        private readonly RestClient _client;

        public FixerClient(IOptions<FixerOptions> fixerOptions)
        {
            _client = new RestClient(fixerOptions.Value.BaseUri)
                .AddDefaultHeader("apikey", fixerOptions.Value.ApiKey);
        }

        /// <inheritdoc />
        public async Task<ConvertResponse?> ConvertAsync(string to, string from, decimal amount)
        {
            var response = await _client.GetJsonAsync<ConvertResponse>(
                $"convert?to={to}&from={from}&amount={amount}");
            return response;
        }
    }
}