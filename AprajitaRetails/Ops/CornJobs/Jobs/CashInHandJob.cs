using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

// private  string Schedule => "*/10 * * * * *";
//      * * * * * *
//    - - - - - -
//    | | | | | |
//    | | | | | +--- day of week(0 - 6) (Sunday=0)
//    | | | | +----- month(1 - 12)
//    | | | +------- day of month(1 - 31)
//    | | +--------- hour(0 - 23)
//    | +----------- min(0 - 59)
//   +------------- sec(0 - 59)

namespace AprajitaRetails.Ops.CornJobs.Jobs
{
    [DisallowConcurrentExecution]
    public sealed class CashInHandJob : IJob, IDisposable
    {
        private readonly ILogger<CashInHandJob> _logger;

        public CashInHandJob(ILogger<CashInHandJob> logger)
        {
            _logger = logger;
        }

        public void Dispose()
        {
            _logger.LogInformation("Example job disposing");
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}