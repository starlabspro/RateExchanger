using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.FixerClient;
using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RateExchanger.Data;
using RateExchanger.Data.Models;

namespace RateExchanger.Features;

public class GetRateCommandHandler : IRequestHandler<GetRateCommand, GetRateResponseDto>
{
    private readonly ILogger<GetRateCommandHandler> _logger;
    private readonly RateExchangerContext _rateExchangerContext;
    private readonly IFixerClient _fixerClient;
    private readonly IOptions<CacheOptions> _cacheOptions;
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly IMapper _mapper;

    public GetRateCommandHandler(
        ILogger<GetRateCommandHandler> logger,
        RateExchangerContext rateExchangerContext,
        IFixerClient fixerClient,
        IEasyCachingProviderFactory factory,
        IOptions<CacheOptions> cacheOptions,
        IMapper mapper)
    {
        _logger = logger;
        _rateExchangerContext = rateExchangerContext;
        _fixerClient = fixerClient;
        _cacheOptions = cacheOptions;
        _mapper = mapper;

        _cachingProvider = factory.GetCachingProvider(_cacheOptions.Value.CacheName);
    }

    public async Task<GetRateResponseDto> Handle(GetRateCommand request, CancellationToken cancellationToken)
    {
        var cachedExchangeRates =
            await _cachingProvider.GetAsync<Dictionary<string, decimal>>(request.BaseCurrency, cancellationToken);

        if (cachedExchangeRates.HasValue)
        {
            var response = _mapper.Map<GetRateResponseDto>(request);
            response.Rates = cachedExchangeRates.Value;
            return response;
        }

        var latestExchangeRates = await _fixerClient.GetLatestAsync(request.BaseCurrency, request.OtherCurrencies);

        if (!latestExchangeRates.Success)
        {
            _logger.LogInformation("Failed to get latest exchange rates.");
        }

        await _cachingProvider.SetAsync(latestExchangeRates.Base, latestExchangeRates.Rates,
            TimeSpan.FromMilliseconds(_cacheOptions.Value.ExpirationTime), cancellationToken);

        await _rateExchangerContext.RateExchanges.AddAsync(new RateExchange
        {
            BaseCurrency = latestExchangeRates.Base,
            Rates = JsonConvert.SerializeObject(latestExchangeRates.Rates),
            CreatedAt = DateTime.UtcNow,
        }, cancellationToken);

        await _rateExchangerContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<GetRateResponseDto>(latestExchangeRates);
    }
}