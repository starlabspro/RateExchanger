using MediatR;

namespace UserRateExchanger.Features;

public record GetUserRateCommand(int UserId, string BaseCurrency, string[] OtherSymbols) : IRequest<GetUserRateResponseDto>;