using Patch.Business.IOC;
using Patch.Business.Services;
using Patch.Data;
using Patch.Data.IOC;
using Patch.Observer.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.RegisterPatchBusiness();
builder.RegisterPatchData();

// builder.Services.AddHostedService<Worker>();
builder.Services.AddQuartz(
    q =>
    {
        q.SchedulerId = "Patch.Observer.Scheduler";

        var ensureLastPatchJobKey = new JobKey("Patch.Observer.EnsureLastPatchJob");
        q.AddJob<EnsureLastPatchJob>(options => options.WithIdentity(ensureLastPatchJobKey));

        // q.AddTrigger(options => options.WithIdentity("Patch.Observer.ImmediateTrigger")
        //     .ForJob(ensureLastPatchJobKey)
        //     .StartNow());

        q.AddTrigger(options => options.WithIdentity("Patch.Observer.IntervalTrigger")
            .ForJob(ensureLastPatchJobKey)
            .WithDailyTimeIntervalSchedule(12, IntervalUnit.Hour));
    }
);
builder.Services.AddQuartzHostedService(
    options =>
    {
        options.AwaitApplicationStarted = true;
        options.WaitForJobsToComplete = true;
    }
);

var host = builder.Build();

host.Services.GetRequiredService<DatabaseInitializer>()
    .Initialize();
using (var scope = host.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<IPatchService>()
        .EnsurePatchHistory();
}

host.Run();
