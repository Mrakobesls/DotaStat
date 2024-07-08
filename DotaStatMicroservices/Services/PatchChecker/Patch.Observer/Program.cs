using PatchChecker.Data;
using PatchChecker.Observer;
using ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton(_ => new DatabaseInitializer(builder.Configuration.GetRequiredConnectionString("DotaStat.Patch")));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Services.GetRequiredService<DatabaseInitializer>();
host.Run();
