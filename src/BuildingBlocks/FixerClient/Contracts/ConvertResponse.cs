using Newtonsoft.Json;

namespace BuildingBlocks.FixerClient.Contracts;

public record ConvertResponse(
    [property: JsonProperty("date")] string Date,
    [property: JsonProperty("historical")] string Historical,
    [property: JsonProperty("info")] Info Info,
    [property: JsonProperty("query")] Query Query,
    [property: JsonProperty("result")] double Result,
    [property: JsonProperty("success")] bool Success
);

public record Info(
    [property: JsonProperty("rate")] double Rate,
    [property: JsonProperty("timestamp")] int Timestamp
);

public record Query(
    [property: JsonProperty("amount")] int Amount,
    [property: JsonProperty("from")] string From,
    [property: JsonProperty("to")] string To
);