using AprajitaRetails.Data;
using Quartz;
using Quartz.Listener;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AprajitaRetails.Ops.CornJobs
{

    /// <summary>
    /// This is Model Class. 
    /// https://www.quartz-scheduler.net/documentation/quartz-3.x/quick-start.html#trying-out-the-application-and-adding-jobs
    /// </summary>
    public class JobSchedular : SchedulerListenerSupport
    {
        private bool running;
        private int errorCount;

        Task<int> FunctionNameAsync(AprajitaRetailsContext  context, CancellationToken cancellationToken)
        {
            return Task.FromResult(-1);
        }
        public override Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            errorCount++;
            return Task.CompletedTask;
        }

        public override Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            running = true;
            return Task.CompletedTask;
        }

        public override Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            running = false;
            return Task.CompletedTask;
        }
    }


    public sealed class ExampleJob : IJob, IDisposable
    {
       // private readonly ILogger<ExampleJob> logger;

        public ExampleJob(/*ILogger<ExampleJob> logger*/)
        {
            //this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            //logger.LogInformation(context.JobDetail.Key + " job executing, triggered by " + context.Trigger.Key);
            await Task.Delay(TimeSpan.FromSeconds(1));
        }

        public void Dispose()
        {
            //logger.LogInformation("Example job disposing");
        }
    }

    public class SampleJobListener : JobListenerSupport
    {
       // private readonly ILogger<SampleJobListener> logger;

        public SampleJobListener(/*ILogger<SampleJobListener> logger*/)
        {
           // this.logger = logger;
        }

        public override string Name => "Sample Job Listener";

        public override Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
           // logger.LogInformation("The job is about to be executed, prepare yourself!");
            return Task.CompletedTask;
        }
    }

    public class SampleTriggerListener : TriggerListenerSupport
    {
        //private readonly ILogger<SampleTriggerListener> logger;

        public SampleTriggerListener(/*ILogger<SampleTriggerListener> logger*/)
        {
           // this.logger = logger;
        }

        public override string Name => "Sample Trigger Listener";

        public override Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
           // logger.LogInformation("Observed trigger fire by trigger {TriggerKey}", trigger.Key);
            return Task.CompletedTask;
        }
    }

    public class SampleSchedulerListener : SchedulerListenerSupport
    {
       // private readonly ILogger<SampleSchedulerListener> logger;

        public SampleSchedulerListener(/*ILogger<SampleSchedulerListener> logger*/)
        {
           // this.logger = logger;
        }

        public override Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
          //  logger.LogInformation("Observed scheduler start");
            return Task.CompletedTask;
        }
    }




}

/*
 * {
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Quartz": {
    "quartz.scheduler.instanceName": "Quartz ASP.NET Core Sample Scheduler"
  }
}
 */

/*
 <?xml version="1.0" encoding="UTF-8"?>

<job-scheduling-data xmlns="http://quartznet.sourceforge.net/JobSchedulingData"
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
 				version="2.0">

  <processing-directives>
    <overwrite-existing-data>true</overwrite-existing-data>
  </processing-directives>

  <schedule>
    
    <job>
      <name>XML Job</name>
      <group>XML Job Group</group>
      <description>Job configured via XML</description>
      <job-type>Quartz.Examples.AspNetCore.ExampleJob, Quartz.Examples.AspNetCore</job-type>
      <durable>true</durable>
      <recover>false</recover>
      <job-data-map>
        <entry>
          <key>key0</key>
          <value>value0</value>
        </entry>
        <entry>
          <key>key1</key>
          <value>value1</value>
        </entry>
        <entry>
          <key>key2</key>
          <value>value2</value>
        </entry>
      </job-data-map>
    </job>
    
    <trigger>
      <simple>
        <name>XML Trigger</name>
        <description>SimpleTriggerDescription</description>
        <job-name>XML Job</job-name>
        <job-group>XML Job Group</job-group>
        <start-time>1982-06-28T18:15:00.0Z</start-time>
        <end-time>2040-05-04T18:13:51.0Z</end-time>
        <misfire-instruction>SmartPolicy</misfire-instruction>
        <repeat-count>100</repeat-count>
        <repeat-interval>3000</repeat-interval>
      </simple>
    </trigger>

  </schedule>
  
</job-scheduling-data>
 */

/*
    // base configuration for DI
            services.AddQuartz(q =>
            {
                // handy when part of cluster or you want to otherwise identify multiple schedulers
                q.SchedulerId = "Scheduler-Core";

                // we take this from appsettings.json, just show it's possible
                // q.SchedulerName = "Quartz ASP.NET Core Sample Scheduler";

                // we could leave DI configuration intact and then jobs need to have public no-arg constructor
                // the MS DI is expected to produce transient job instances
                q.UseMicrosoftDependencyInjectionJobFactory(options =>
                {
                    // if we don't have the job in DI, allow fallback to configure via default constructor
                    options.AllowDefaultConstructor = true;
                });

                // or
                // q.UseMicrosoftDependencyInjectionScopedJobFactory();

                // these are the defaults
                q.UseSimpleTypeLoader();
                q.UseInMemoryStore();
                q.UseDefaultThreadPool(tp =>
                {
                    tp.MaxConcurrency = 10;
                });

                // quickest way to create a job with single trigger is to use ScheduleJob
                q.ScheduleJob<ExampleJob>(trigger => trigger
                    .WithIdentity("Combined Configuration Trigger")
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(7)))
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                    .WithDescription("my awesome trigger configured for a job with single call")
                );

                // you can also configure individual jobs and triggers with code
                // this allows you to associated multiple triggers with same job
                // (if you want to have different job data map per trigger for example)
                q.AddJob<ExampleJob>(j => j
                    .StoreDurably() // we need to store durably if no trigger is associated
                    .WithDescription("my awesome job")
                );

                // here's a known job for triggers
                var jobKey = new JobKey("awesome job", "awesome group");
                q.AddJob<ExampleJob>(jobKey, j => j
                    .WithDescription("my awesome job")
                );

                q.AddTrigger(t => t
                    .WithIdentity("Simple Trigger")
                    .ForJob(jobKey)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever())
                    .WithDescription("my awesome simple trigger")
                );

                q.AddTrigger(t => t
                    .WithIdentity("Cron Trigger")
                    .ForJob(jobKey)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(3)))
                    .WithCronSchedule("0/3 * * * * ?")
                    .WithDescription("my awesome cron trigger")
                );

                const string calendarName = "myHolidayCalendar";
                q.AddCalendar<HolidayCalendar>(
                    name: calendarName,
                    replace: true,
                    updateTriggers: true,
                    x => x.AddExcludedDate(new DateTime(2020, 5, 15))
                );

                q.AddTrigger(t => t
                    .WithIdentity("Daily Trigger")
                    .ForJob(jobKey)
                    .StartAt(DateBuilder.EvenSecondDate(DateTimeOffset.UtcNow.AddSeconds(5)))
                    .WithDailyTimeIntervalSchedule(x => x.WithInterval(10, IntervalUnit.Second))
                    .WithDescription("my awesome daily time interval trigger")
                    .ModifiedByCalendar(calendarName)
                );

                // also add XML configuration and poll it for changes
                q.UseXmlSchedulingConfiguration(x =>
                {
                    x.Files = new[] { "~/quartz_jobs.config" };
                    x.ScanInterval = TimeSpan.FromMinutes(1);
                    x.FailOnFileNotFound = true;
                    x.FailOnSchedulingError = true;
                });

                // convert time zones using converter that can handle Windows/Linux differences
                q.UseTimeZoneConverter();

                // add some listeners
                q.AddSchedulerListener<SampleSchedulerListener>();
                q.AddJobListener<SampleJobListener>(GroupMatcher<JobKey>.GroupEquals(jobKey.Group));
                q.AddTriggerListener<SampleTriggerListener>();

                // example of persistent job store using JSON serializer as an example
                /*
                q.UsePersistentStore(s =>
                {
                    s.UseProperties = true;
                    s.RetryInterval = TimeSpan.FromSeconds(15);
                    s.UseSqlServer(sqlServer =>
                    {
                        sqlServer.ConnectionString = "some connection string";
                        // this is the default
                        sqlServer.TablePrefix = "QRTZ_";
                    });
                    s.UseJsonSerializer();
                    s.UseClustering(c =>
                    {
                        c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                        c.CheckinInterval = TimeSpan.FromSeconds(10);
                    });
                });
                */
/*
            });

            // ASP.NET Core hosting
            services.AddQuartzServer(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            services
                .AddHealthChecksUI()
                .AddInMemoryStorage();

https://github.com/quartznet/quartznet/tree/master/src/Quartz.Examples.AspNetCore
 */
