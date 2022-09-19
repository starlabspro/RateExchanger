using MediatR;

namespace UserRateExchanger.Features;

public class GetUserRateCommand : IRequest<GetUserRateResponseDto>
{
    public int UserId { get; set; }
    public string BaseCurrency { get; set; }
    public string[] OtherCurrencies { get; set; }
}