using AutoMapper;
using BuildingBlocks.Caching;
using BuildingBlocks.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UserRateExchanger.Features;

public class GetUserRateCommandHandler : IRequestHandler<GetUserRateCommand, GetUserRateResponseDto>
{
    private readonly ILogger<GetUserRateCommandHandler> _logger;
    private readonly IRateExchangerService _rateExchangerService;
    private readonly IMapper _mapper;
    private readonly ICacheManager<List<DateTime>> _cacheManager;

    public GetUserRateCommandHandler(
        ILogger<GetUserRateCommandHandler> logger,
        IRateExchangerService rateExchangerService,
        IMapper mapper,
        ICacheManager<List<DateTime>> cacheManager)
    {
        _logger = logger;
        _rateExchangerService = rateExchangerService;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<GetUserRateResponseDto> Handle(GetUserRateCommand command, CancellationToken cancellationToken)
    {
        var isValid = await _cacheManager.IsValidAsync($"{command.UserId}", cancellationToken);

        if (!isValid)
        {
            throw new Exception($"Limit exceeded for user {command.UserId}");
        }

        var exchangeRates =
            await _rateExchangerService.GetExchangeRateAsync(_mapper.Map<GetExchangeRateRequest>(command));

        await _cacheManager.UpdateAsync($"{command.UserId}", null, cancellationToken);

        _logger.LogInformation("User {UserId} got the latest exchange rates for {CurrencyCode}",
            command.UserId, command.BaseCurrency);

        return _mapper.Map<GetUserRateResponseDto>(exchangeRates);
    }
}