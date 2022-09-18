using MediatR;

namespace RateExchanger.Features;

public record GetRateCommand(string To, string From, decimal Amount) : IRequest<GetRateResponseDto>;