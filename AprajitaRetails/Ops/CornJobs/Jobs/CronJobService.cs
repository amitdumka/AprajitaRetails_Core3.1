using AprajitaRetails.Data;
using AprajitaRetails.Ops.CornJobs.JobHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using System;
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
    public class CronJobService : BackgroundService
    {
        private CrontabSchedule _schedule;
        private CrontabSchedule _scheduleForCashCorrection;
        
        private DateTime _nextRun;
        private DateTime _nextRunForCashCorrection;

        private readonly int StoreId = 1;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CronJobService> _logger;

       // private string Schedule => "*/30 * * * * *"; //Runs every 10 seconds
        private string Schedule => "0 15 10 * * *"; //Runs every day on 10:15
        private string ScheduleForCashCorrection => "0 10 00 * * *"; //Runs every day on 10:15
        public CronJobService(ILogger<CronJobService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            _schedule = CrontabSchedule.Parse (Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _scheduleForCashCorrection = CrontabSchedule.Parse(ScheduleForCashCorrection, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            
            _nextRun = _schedule.GetNextOccurrence (DateTime.Now);    
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence (now);
                if ( now > nextrun )//_nextRun
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();
                        
                        await JobHelper.CheckTodayAttendanceAsync(Cdb, StoreId);

                    }

                    _nextRun = _schedule.GetNextOccurrence (DateTime.Now);
                }
                await Task.Delay (5000, stoppingToken); //5 seconds delay

                 nextrun = _scheduleForCashCorrection.GetNextOccurrence(now);
                if (now > nextrun)//_nextRun
                {
                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();

                        JobHelper.CorrectCashInHand(Cdb, StoreId);

                    }

                    _nextRunForCashCorrection = _scheduleForCashCorrection.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay


            }
            while ( !stoppingToken.IsCancellationRequested );
        }

       
    }


}
