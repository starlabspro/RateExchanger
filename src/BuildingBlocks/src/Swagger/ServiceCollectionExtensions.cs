using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BuildingBlocks.Swagger;

/// <summary>
/// The Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Sets up the Swagger API info and documentation.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection SetupSwaggerGen(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerSection = configuration.GetSection("Swagger");

        services.Configure<SwaggerOptions>(swaggerSection);

        var swaggerOptions = swaggerSection.Get<SwaggerOptions>();

        return services.AddSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = swaggerOptions?.Version ?? "v1",
                    Title = swaggerOptions?.Title ?? "Swagger API Docs"
                });
            });
    }
}