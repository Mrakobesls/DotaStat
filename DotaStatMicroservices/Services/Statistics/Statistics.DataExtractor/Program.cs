using Statistics.Business.IOC;
using Statistics.DataExtractor;
using Statistics.DataExtractor.Configuration;

var builder = Host.CreateApplicationBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new KeyNotFoundException();
builder.Services.RegisterStatistics(connection);
builder.Services.AddHostedService<Worker>();
builder.Services.Configure<StartupConfiguration>(builder.Configuration.GetSection("Startup"));

var host = builder.Build();
host.Run();
