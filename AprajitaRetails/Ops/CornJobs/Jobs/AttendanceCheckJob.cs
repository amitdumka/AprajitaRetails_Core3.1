using AprajitaRetails.Data;
using AprajitaRetails.Ops.CornJobs.JobHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public sealed class AttendanceCheckJob : IJob, IDisposable
    {
      
        private readonly ILogger<AttendanceCheckJob> _logger;
        private readonly AprajitaRetailsContext db;
        private readonly int StoreId = 1;

        public AttendanceCheckJob(ILogger<AttendanceCheckJob> logger, AprajitaRetailsContext _db)
        {
            _logger = logger;
            db = _db;
            
        }

        public void Dispose()
        {
            _logger.LogInformation("Dispoing");
            this.Dispose();
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Attadance checked!");
            _ = JobHelper.CheckTodayAttendanceAsync(db, StoreId);
            return Task.CompletedTask;
        }
    }

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
    public class CronJobService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private readonly int StoreId = 1;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CronJobService> _logger;

       // private string Schedule => "*/30 * * * * *"; //Runs every 10 seconds
        private string Schedule => "0 15 10 * * *"; //Runs every day on 10:15
        public CronJobService(ILogger<CronJobService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
            _schedule = CrontabSchedule.Parse (Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence (DateTime.Now);
      
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence (now);
                if ( now > _nextRun )
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();
                        
                        await JobHelper.CheckTodayAttendanceAsync(Cdb, StoreId);

                    }

                    _nextRun = _schedule.GetNextOccurrence (DateTime.Now);
                }
                await Task.Delay (5000, stoppingToken); //5 seconds delay
            }
            while ( !stoppingToken.IsCancellationRequested );
        }

       
    }


}
