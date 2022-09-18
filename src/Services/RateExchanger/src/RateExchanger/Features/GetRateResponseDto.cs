namespace RateExchanger.Features;

public class GetRateResponseDto
{
    public string Currency { get; set; }
    public decimal Rate { get; set; }
}