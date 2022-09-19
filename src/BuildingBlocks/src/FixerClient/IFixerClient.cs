using BuildingBlocks.FixerClient.Contracts;

namespace BuildingBlocks.FixerClient;

/// <summary>
/// The Fixer Client.
/// </summary>
public interface IFixerClient
{
    /// <summary>
    /// Calls Fixer to get the latest exchange rates.
    /// </summary>
    /// <param name="baseCurrency">The Base Currency.</param>
    /// <param name="symbols">The Currency symbols.</param>
    /// <returns>The <see cref="GetLatestRatesResponse"/>.</returns>
    Task<GetLatestRatesResponse> GetLatestAsync(string baseCurrency, params string[] symbols);
}