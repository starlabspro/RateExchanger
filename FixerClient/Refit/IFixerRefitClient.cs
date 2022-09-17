using Refit;

namespace FixerClient.Refit
{
    public interface IFixerRefitClient
    {
        [Get("/api/latest")]
        Task<string> GetLatestData();
    }
}
