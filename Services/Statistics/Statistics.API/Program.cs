using EventBus.Abstractions;
using Microsoft.AspNetCore.Authorization;
using ServiceDefaults;
using Statistics.Business.IntegrationEvents.EventHandlers;
using Statistics.Business.IntegrationEvents.Events;
using Statistics.Business.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddControllers();
builder.RegisterStatisticsBusiness();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
// builder.Services.AddAuthentication("Bearer")
//     .AddJwtBearer("Bearer", options =>
//     {
//         // URL of our identity server
//         options.Authority = "https://identity-api:5041";
//         // HTTPS required for the authority (defaults to true but disabled for development).
//         options.RequireHttpsMetadata = false;
//         // the name of this API - note: matches the API resource name configured above
//         options.Audience = "statistics";
//     });

var app = builder.Build();

app.UseServiceDefaults();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

// app.UseAuthentication();
// app.UseServiceDefaults();

// var eventBus = app.Services.GetRequiredService<IEventBus>();
//
// eventBus.Subscribe<NewHeroesReleasedIntegrationEvent, NewHeroesReleasedIntegrationEventHandler>();

app.MapGet("/TextOk", () => "Really works!");
app.MapGet("/Text", [Authorize]() => "Really works!");
app.MapControllers();

app.Run();
