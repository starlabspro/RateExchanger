namespace FixerClient
{
    public interface IFixerProvider
    {
        Task<string> GetLatestData();
    }
}
