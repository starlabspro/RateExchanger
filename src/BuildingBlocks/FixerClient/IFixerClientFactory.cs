using BuildingBlocks.FixerClient.Contracts;

namespace BuildingBlocks.FixerClient;

public interface IFixerClient
{
    Task<ConvertResponse?> ConvertAsync(string to, string from, decimal amount);
}