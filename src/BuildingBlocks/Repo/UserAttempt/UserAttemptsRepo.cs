using BuildingBlocks.Caching.Contract;
using BuildingBlocks.EFCore;
using BuildingBlocks.Repo.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocks.Repo.UserAttempt
{
    public class UserAttemptsRepo: IUserAttemptsRepo
    {
        private readonly RateExchangerDbContext _context;
        private readonly ICacheManager _cacheManager;
        public UserAttemptsRepo(RateExchangerDbContext context, ICacheManager cacheManager)
        {
            _context = context;
            _cacheManager = cacheManager;
        }

        public void Insert(UserAttempts item)
        {
            _cacheManager.Insert(item.Id);
            _context.AddAsync(item);
        }
    }
}
