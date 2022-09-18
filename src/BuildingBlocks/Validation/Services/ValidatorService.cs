using BuildingBlocks.Caching.Contract;
using BuildingBlocks.Validation.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Validation.Service
{
    public class ValidatorService : IValidatorService
    {
        private const string keyPrefix = "user";
        private readonly ICacheManager _cacheManager;
        const int timeWillExpire = 60;

        public ValidatorService(ICacheManager cacheManager)
        {
            _cacheManager=cacheManager;
        }

        public bool ValidateRequestOfTheUser(int userId)
        {
            var userCacheKey = keyPrefix+userId;
            var attempts = (int)_cacheManager.Get(userCacheKey);

            if (attempts == null && attempts <= 10)
            {
                return true;
            }
            return false;
        }
    }
}