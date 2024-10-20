using Patch.Business.IOC;
using Patch.Data.IOC;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();

builder.RegisterPatchBusiness();
builder.RegisterPatchData();

var app = builder.Build();

app.UseServiceDefaults();

app.UseRouting()
    .UseEndpoints(
        endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: "{action}")
                .WithOpenApi();
        }
    );

app.Run();
