using EventBus.Abstractions;
using Heroes.API.IntegrationEvents.EventHandlers;
using Heroes.API.IntegrationEvents.Events;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddTransient<PingCalledIntegrationEventHandler>();

var app = builder.Build();

app.UseServiceDefaults();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<PingCalledIntegrationEvent, PingCalledIntegrationEventHandler>();

app.Run();
