using AprajitaRetails.Data;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.CornJobs.Jobs
{
    [DisallowConcurrentExecution]
    public sealed class AttendanceCheckJob : IJob, IDisposable
    {
      
        private readonly ILogger<AttendanceCheckJob> _logger;
        private readonly AprajitaRetailsContext db;
        private readonly int StoreId = 1;

        public AttendanceCheckJob(ILogger<AttendanceCheckJob> logger, AprajitaRetailsContext _db, int storeID=1)
        {
            _logger = logger;
            db = _db;
            StoreId = storeID;
        }

        public void Dispose()
        {
            _logger.LogInformation("Dispoing");
            this.Dispose();
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Attadance checked!");
            JobHelper.JobHelper.CheckTodayAttendance(db, StoreId);
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


}
