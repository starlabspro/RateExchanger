using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuildingBlocks.Caching.Contract;
using BuildingBlocks.Caching.Service;
using BuildingBlocks.Repo.Contracts;
using BuildingBlocks.Repo.UserAttempt;
using BuildingBlocks.Validation.Contracts;
using BuildingBlocks.Validation.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Modules
{
    public static class CoreModules
    {
        public static void Load (IServiceCollection services)
        {
            services.AddTransient<ICacheManager, CacheManager>();
            services.AddTransient<IUserAttemptsRepo, UserAttemptsRepo>();
            services.AddTransient<IValidatorService, ValidatorService>();
        }
    }
}
