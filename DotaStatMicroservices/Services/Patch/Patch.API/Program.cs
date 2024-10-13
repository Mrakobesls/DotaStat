using Patch.API.IntegrationEvents;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddTransient<IPatchIntegrationEventService, PatchIntegrationEventService>();

var app = builder.Build();

app.UseServiceDefaults();

app.UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{action}");
    });
// app.MapControllers()
//     .WithOpenApi();
// app.MapGet(
//         "/Ping",
//         () =>
//         {
//             return DateTime.Now;
//         }
//     )
//     .WithName("Ping")
//     .WithOpenApi();

app.Run();
