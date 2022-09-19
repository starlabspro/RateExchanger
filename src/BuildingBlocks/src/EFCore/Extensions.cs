using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BuildingBlocks.EFCore;

public static class Extensions
{
    public static IServiceCollection AddDbContext<TContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TContext : DbContext, IDbContext
    {
        services.AddDbContext<TContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(typeof(TContext).Assembly.GetName().Name)));

        services.AddScoped<IDbContext>(provider => provider.GetService<TContext>());

        return services;
    }

    public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app, IWebHostEnvironment env)
        where TContext : DbContext, IDbContext
    {
        MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();

        if (!env.IsEnvironment("test"))
            SeedDataAsync(app.ApplicationServices).GetAwaiter().GetResult();

        return app;
    }

    private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider)
        where TContext : DbContext, IDbContext
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        await context.Database.MigrateAsync();
    }

    private static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();
        foreach (var seeder in seeders)
        {
            await seeder.SeedDataAsync();
        }
    }
}