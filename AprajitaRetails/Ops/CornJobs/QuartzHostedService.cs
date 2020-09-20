using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.CornJobs
{
    public class QuartzHostedService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly JobMetadata jobMetadata;

        public QuartzHostedService(ISchedulerFactory
            schedulerFactory,
            JobMetadata jobMetadata,
            IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobMetadata = jobMetadata;
            this.jobFactory = jobFactory;
        }

        public IScheduler Scheduler { get; set; }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await schedulerFactory.GetScheduler();
            Scheduler.JobFactory = jobFactory;
            var job = CreateJob(jobMetadata);
            var trigger = CreateTrigger(jobMetadata);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            await Scheduler.Start(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        private ITrigger CreateTrigger(JobMetadata jobMetadata)
        {
            return TriggerBuilder.Create()
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithCronSchedule(jobMetadata.CronExpression)
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }

        private IJobDetail CreateJob(JobMetadata jobMetadata)
        {
            return JobBuilder
            .Create(jobMetadata.JobType)
            .WithIdentity(jobMetadata.JobId.ToString())
            .WithDescription($"{jobMetadata.JobName}")
            .Build();
        }
    }

    public class QuartzJobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle triggerFiredBundle,
        IScheduler scheduler)
        {
            var jobDetail = triggerFiredBundle.JobDetail;
            return (IJob)_serviceProvider.GetService(jobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}