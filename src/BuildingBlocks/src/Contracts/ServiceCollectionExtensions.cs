using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Contracts;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRateExchangerService(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var configurationSection = configuration.GetSection("RateExchanger");

        services.Configure<RateExchangerOptions>(configurationSection);

        services.AddTransient<IRateExchangerService, RateExchangerService>();

        return services;
    }
}