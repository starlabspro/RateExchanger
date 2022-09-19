namespace UserRateExchanger.Features;

public class GetUserRateResponseDto
{
    public string BaseCurrency { get; set; }

    public Dictionary<string, decimal> Rates { get; set; }
}