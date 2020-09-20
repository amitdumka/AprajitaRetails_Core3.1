using AprajitaRetails.Data;
using AprajitaRetails.Ops.CornJobs.JobHelpers;
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
}