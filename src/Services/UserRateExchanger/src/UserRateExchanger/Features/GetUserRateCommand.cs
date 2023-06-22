using MediatR;

namespace UserRateExchanger.Features;

public class GetUserRateCommand : IRequest<GetUserRateResponseDto>
{
    /// <summary>
    /// The User Id.
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// The Base Currency.
    /// </summary>
    public string BaseCurrency { get; set; }
    
    /// <summary>
    /// The Other Currencies.
    /// </summary>
    public string[] OtherCurrencies { get; set; }
}