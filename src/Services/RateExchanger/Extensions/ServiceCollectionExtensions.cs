using BuildingBlocks.Hangfire;
using BuildingBlocks.Hangfire.Jobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace RateExchanger.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Sets up the Swagger API info and documentation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection SetupSwaggerGen(this IServiceCollection services)
    {
        return services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "RateExchanger API"
                });
            });
    }

    public static void AddRateExchangerJob(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HangFireOptions>(configuration.GetSection("HangFire"));

        services.AddSingleton<RateExchangerJob>();
    }
}