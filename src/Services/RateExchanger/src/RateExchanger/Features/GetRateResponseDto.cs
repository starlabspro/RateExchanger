namespace RateExchanger.Features;

public class GetRateResponseDto
{
    public string BaseCurrency { get; set; }

    public Dictionary<string, decimal> Rates { get; set; }
}