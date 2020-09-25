using AprajitaRetails.Data;
using AprajitaRetails.Ops.CornJobs.JobHelpers;
using AprajitaRetails.Ops.TAS.Mails;
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
        private CrontabSchedule _scheduleForPaySlip;
        private DateTime _nextRunForPaySlip;
        private string ScheduleForPaySlip => "0 00 00 2 * *"; //Runs every day on 10:15 IST and 4:45 GMT

        private CrontabSchedule _schedule;
        private CrontabSchedule _scheduleForCashCorrection;

        private DateTime _nextRun;
        private DateTime _nextRunForCashCorrection;

        private readonly int StoreId = 1;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<CronJobService> _logger;

        // private string Schedule => "*/30 * * * * *"; //Runs every 10 seconds
        private string Schedule => "0 45 4 * * *"; //Runs every day on 10:15 IST and 4:45 GMT

        private string ScheduleForCashCorrection => "0 10 00 * * *"; //Runs every day on 00:10 GMT

        public CronJobService(ILogger<CronJobService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;

            _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _scheduleForCashCorrection = CrontabSchedule.Parse(ScheduleForCashCorrection, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _scheduleForPaySlip = CrontabSchedule.Parse(ScheduleForPaySlip, new CrontabSchedule.ParseOptions { IncludingSeconds = true });

            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _nextRunForCashCorrection = _scheduleForCashCorrection.GetNextOccurrence(DateTime.Now);
            _nextRunForPaySlip = _scheduleForPaySlip.GetNextOccurrence(DateTime.Now);
          //  MyMail.SendEmail($"CronJob Service Creation. {DateTime.Now.ToString()}", $" Attendance Checker on {_nextRun.ToString()}.", "amitnarayansah@gmail.com");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            try
            {

                do
                {
                    var now = DateTime.Now;
                    var nextrun = _schedule.GetNextOccurrence(now);
                    if (now > _nextRun)//_nextRun
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();
                            await JobHelper.CheckTodayAttendanceAsync(Cdb, StoreId);
                        }

                        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                        MyMail.SendEmail("CronJob Service Execution of AttandenceChecker ", $"Attendance Checker on {_nextRun.ToString()}.", "amitnarayansah@gmail.com");
                    }
                    await Task.Delay(50000, stoppingToken); //5 seconds delay

                    var nextrunforcash = _scheduleForCashCorrection.GetNextOccurrence(now);
                    if (now > _nextRunForCashCorrection)//_nextRun
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();
                            JobHelper.CorrectCashInHand(Cdb, StoreId);
                        }

                        _nextRunForCashCorrection = _scheduleForCashCorrection.GetNextOccurrence(DateTime.Now);
                        MyMail.SendEmail("CronJob Service Execution of Cash Correction ", $"Cash Correction  on {_nextRunForCashCorrection.ToString()}.", "amitnarayansah@gmail.com");
                    }
                    await Task.Delay(50000, stoppingToken); //5 seconds delay


                    var nextrunforPaySlip = _scheduleForPaySlip.GetNextOccurrence(now);
                    if (now > nextrunforPaySlip)//_nextRun
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var Cdb = scope.ServiceProvider.GetRequiredService<AprajitaRetailsContext>();
                            JobHelper.GeneratePaySlip(Cdb);
                        }

                        _nextRunForPaySlip = _scheduleForPaySlip.GetNextOccurrence(DateTime.Now);
                        MyMail.SendEmail("CronJob Service Execution of PaySlip  ", $"PaySlip   on {_nextRunForPaySlip.ToString()}.", "amitnarayansah@gmail.com");
                    }
                    await Task.Delay(50000, stoppingToken); //5 seconds delay
                }
                while (!stoppingToken.IsCancellationRequested);
            }
            catch (Exception ex)
            {
                MyMail.SendEmail($"Error On Cron Serive Ex . {DateTime.Now.ToString()}", $"Error Occured!.Msg= {ex.Message}\n Inner Exp= {ex.InnerException}\n Stack Tracce= {ex.StackTrace} ", "amitnarayansah@gmail.com");
              
            }
        }
    }
}