using MediatR;
using RateExchanger.Data;
using RateExchanger.Models;

namespace RateExchanger.Features;

public class GetRateCommandHandler : IRequestHandler<GetRateCommand, GetRateResponseDto>
{
    private readonly RateExchangerContext _rateExchangerContext;

    public GetRateCommandHandler(RateExchangerContext rateExchangerContext)
    {
        _rateExchangerContext = rateExchangerContext;
    }

    public async Task<GetRateResponseDto> Handle(GetRateCommand request, CancellationToken cancellationToken)
    {
        await _rateExchangerContext.RateExchanges.AddAsync(new RateExchange()
        {
            Rate = "1.0",
            CreatedAt = DateTime.UtcNow,
            ToCurrency = "eur",
            FromCurrency = "usd",

        });
        return new GetRateResponseDto();
    }
}