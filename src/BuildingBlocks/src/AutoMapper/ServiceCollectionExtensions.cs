using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.AutoMapper;

/// <summary>
/// The Service Collection Extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add AutoMapper to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <param name="assemblies">The list of <see cref="Assembly"/>.</param>
    /// <returns></returns>
    public static IServiceCollection AddAutoMapperAssemblies(this IServiceCollection services, List<Assembly> assemblies)
    {
        services.AddAutoMapper(assemblies);

        return services;
    }
}