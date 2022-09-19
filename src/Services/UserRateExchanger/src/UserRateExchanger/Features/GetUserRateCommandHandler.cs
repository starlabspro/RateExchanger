using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.Contracts;
using EasyCaching.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace UserRateExchanger.Features;

public class GetUserRateCommandHandler : IRequestHandler<GetUserRateCommand, GetUserRateResponseDto>
{
    private readonly ILogger<GetUserRateCommandHandler> _logger;
    private readonly IOptions<CacheOptions> _cacheOptions;
    private readonly IEasyCachingProvider _cachingProvider;
    private readonly IRateExchangerService _rateExchangerService;
    private readonly IMapper _mapper;

    public GetUserRateCommandHandler(
        ILogger<GetUserRateCommandHandler> logger,
        IOptions<CacheOptions> cacheOptions,
        IEasyCachingProvider cachingProvider,
        IRateExchangerService rateExchangerService,
        IMapper mapper)
    {
        _logger = logger;
        _cacheOptions = cacheOptions;
        _cachingProvider = cachingProvider;
        _rateExchangerService = rateExchangerService;
        _mapper = mapper;
    }

    public async Task<GetUserRateResponseDto> Handle(GetUserRateCommand command, CancellationToken cancellationToken)
    {
        var cachedUserRequests =
            await _cachingProvider.GetAsync<List<DateTime>>($"{command.UserId}", cancellationToken);

        ValidateUserRequestLimit(command, cachedUserRequests);

        var exchangeRates =
            await _rateExchangerService.GetExchangeRateAsync(_mapper.Map<GetExchangeRateRequest>(command));

        await AddLatestRequestDateTimestampAsync(command.UserId, cachedUserRequests, cancellationToken);

        _logger.LogInformation("User {UserId} got the latest exchange rates for {CurrencyCode}",
            command.UserId, command.BaseCurrency);

        return _mapper.Map<GetUserRateResponseDto>(exchangeRates);
    }

    private void ValidateUserRequestLimit(GetUserRateCommand command, CacheValue<List<DateTime>> cachedUserRequests)
    {
        if (!cachedUserRequests.HasValue) return;

        var lowerDateTimeLimit = DateTime.UtcNow.AddMilliseconds(-_cacheOptions.Value.ExpirationTime);

        var numberOfRequestsForTimeLimit =
            cachedUserRequests.Value.Where(x => x >= lowerDateTimeLimit);

        if (numberOfRequestsForTimeLimit.Count() > 10)
        {
            _logger.LogError("User {UserId} exceeded the maximum amount of requests within {TimeLimit}",
                command.UserId, _cacheOptions.Value.ExpirationTime);
            throw new Exception("Limit exceeded");
        }
    }

    private async Task AddLatestRequestDateTimestampAsync(
        int userId,
        CacheValue<List<DateTime>> cachedUserRequestsDateTimestamps,
        CancellationToken cancellationToken)
    {
        // Initialize the list if it's null
        var dateTimestamps = cachedUserRequestsDateTimestamps.HasValue
            ? cachedUserRequestsDateTimestamps.Value
            : new List<DateTime>();

        dateTimestamps.Add(DateTime.UtcNow);
        await _cachingProvider.SetAsync($"{userId}", dateTimestamps,
            TimeSpan.FromMilliseconds(_cacheOptions.Value.ExpirationTime), cancellationToken);
    }
}