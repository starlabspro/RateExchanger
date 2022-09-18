using BuildingBlocks.FixerClient.Contracts;

namespace BuildingBlocks.FixerClient;

/// <summary>
/// The Fixer Client.
/// </summary>
public interface IFixerClient
{
    /// <summary>
    /// Calls Fixer to convert the amount from one currency to another.
    /// </summary>
    /// <param name="to">The currency to convert to.</param>
    /// <param name="from">The currency to convert from.</param>
    /// <param name="amount">The amount to be converted.</param>
    /// <returns>The <see cref="ConvertResponse"/>.</returns>
    Task<ConvertResponse?> ConvertAsync(string to, string from, decimal amount);
}