using System.Text.Json.Serialization;

namespace BuildingBlocks.FixerClient.Contracts;

public record GetLatestRatesResponse(
    [property: JsonPropertyName("base")] string Base,
    [property: JsonPropertyName("date")] string Date,
    [property: JsonPropertyName("rates")] Dictionary<string, decimal> Rates,
    [property: JsonPropertyName("success")] bool Success,
    [property: JsonPropertyName("timestamp")] int Timestamp
);