using BuildingBlocks.Hangfire;
using BuildingBlocks.Hangfire.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace RateExchanger.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseRateExchangerJob(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var rateExchangerJob = serviceScope.ServiceProvider.GetService<RateExchangerJob>();
                rateExchangerJob.Process();
            }
        }
    }
}
