using MediatR;

namespace RateExchanger.Features;

public record GetRateCommand(string BaseCurrency, string[] OtherCurrencies) : IRequest<GetRateResponseDto>;