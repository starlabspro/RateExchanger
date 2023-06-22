namespace UserRateExchanger.Features;

public class GetUserRateResponseDto
{
    /// <summary>
    /// The Base Currency.
    /// </summary>
    public string BaseCurrency { get; set; }

    /// <summary>
    /// The Rates.
    /// </summary>
    public Dictionary<string, decimal> Rates { get; set; }
}