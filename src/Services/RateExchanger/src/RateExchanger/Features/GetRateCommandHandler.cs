using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.FixerClient;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RateExchanger.Data;
using RateExchanger.Data.Models;

namespace RateExchanger.Features;

public class GetRateCommandHandler : IRequestHandler<GetRateCommand, GetRateResponseDto>
{
    private readonly ILogger<GetRateCommandHandler> _logger;
    private readonly RateExchangerContext _rateExchangerContext;
    private readonly IFixerClient _fixerClient;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Dictionary<string, decimal>> _cacheManager;

    public GetRateCommandHandler(
        ILogger<GetRateCommandHandler> logger,
        RateExchangerContext rateExchangerContext,
        IFixerClient fixerClient,
        IMapper mapper,
        ICacheManager<Dictionary<string, decimal>> cacheManager)
    {
        _logger = logger;
        _rateExchangerContext = rateExchangerContext;
        _fixerClient = fixerClient;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<GetRateResponseDto> Handle(GetRateCommand request, CancellationToken cancellationToken)
    {
        var cachedValues = await _cacheManager.GetAsync(request.BaseCurrency, cancellationToken);
        if (cachedValues != null)
        {
            var response = _mapper.Map<GetRateResponseDto>(request);
            response.Rates = cachedValues;
            return response;
        }

        var latestExchangeRates = await _fixerClient.GetLatestAsync(request.BaseCurrency, request.OtherCurrencies);

        if (!latestExchangeRates.Success)
        {
            _logger.LogInformation("Failed to get latest exchange rates.");
        }

        await _cacheManager.UpdateAsync(request.BaseCurrency, latestExchangeRates.Rates, cancellationToken);

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