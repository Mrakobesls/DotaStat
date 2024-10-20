using EventBus.Abstractions;
using Hero.Business.IntegrationEvents.EventHandlers;
using Hero.Business.IntegrationEvents.Events;
using Hero.Business.IOC;
using Hero.Data.IOC;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.RegisterHeroBusiness();
builder.RegisterHeroData();

var app = builder.Build();

app.UseServiceDefaults();

var eventBus = app.Services.GetRequiredService<IEventBus>();

eventBus.Subscribe<PingCalledIntegrationEvent, PingCalledIntegrationEventHandler>();
eventBus.Subscribe<PatchHistoryUpdatedIntegrationEvent, PatchHistoryUpdatedIntegrationEventHandler>();

app.Run();
