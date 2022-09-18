using FixerClient.Refit;

namespace FixerClient
{
    public class FixerProvider : IFixerProvider
    {
        private readonly IFixerRefitClient _fixerRefitClient;
        public FixerProvider(IFixerRefitClient fixerRefitClient)
        {
            _fixerRefitClient = fixerRefitClient;
        }

        public async Task<string> GetLatestData()
        {
            var data = await _fixerRefitClient.GetLatestData();
            return data;
        }
    }
}