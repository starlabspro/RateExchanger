using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.AutoMapper;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAutoMapperAssemblies(this IServiceCollection services, List<Assembly> assemblies)
    {
        services.AddAutoMapper(assemblies);

        return services;
    }
}