using BuildingBlocks.Options;
using Hangfire;
using Hangfire.Common;
using Microsoft.Extensions.Options;

namespace BuildingBlocks.Jobs
{
    public class RateExchangerJob
    {
        private readonly HangFireOptions _hangFireOptions;
        private readonly IRecurringJobManager _recurringJobManager;

        public RateExchangerJob (IOptions<HangFireOptions> hangFireOptions, IRecurringJobManager recurringJobManager)
        {
            _hangFireOptions = hangFireOptions.Value;
            _recurringJobManager = recurringJobManager;
        }

        public void Process()
        {
            //if (_hangFireOptions.Process.Enabled)
            //{
            //    _recurringJobManager.AddOrUpdate(_hangFireOptions.Process.Name, Job.FromExpression<>(
            //        m => m.method()), _hangFireOptions.Process.CronInterval);
            //}
        }
    }
}
