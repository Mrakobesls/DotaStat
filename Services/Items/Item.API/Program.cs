using EventBus.Abstractions;
using Item.Business.Infrastructure;
using Item.Business.IntegrationEvents.EventHandlers;
using Item.Business.IntegrationEvents.Events;
using Item.Business.IOC;
using Item.Business.Services;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.RegisterItemBusiness();

var app = builder.Build();

app.UseServiceDefaults();

app.MapGet("/TextOk", () => "Works!");

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<PatchHistoryUpdatedIntegrationEvent, PatchHistoryUpdatedIntegrationEventHandler>();

app.Services.GetRequiredService<DatabaseInitializer>()
    .Initialize();
using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<IItemCommands>()
        .EnsureAllExist();
}

app.Run();
