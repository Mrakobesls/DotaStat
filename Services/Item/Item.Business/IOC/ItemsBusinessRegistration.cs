using CommonSources.OpenDota;
using EventBus.Abstractions;
using Item.Business.Infrastructure;
using Item.Business.IntegrationEvents;
using Item.Business.IntegrationEvents.EventHandlers;
using Item.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDefaults;

namespace Item.Business.IOC;

public static class ItemsBusinessRegistration
{
    public static void RegisterItemBusiness(this IHostApplicationBuilder builder)
    {
        // builder.AddSteamHttpClient<SteamHttpClient>();
        builder.AddOpenDotaHttpClient<OpenDotaHttpClient>();

        builder.Services.AddScoped<IItemCommands, ItemCommands>();
        builder.Services.AddScoped<IItemQueries, ItemQueries>();
        builder.Services.AddScoped<IIntegrationEventService, ItemIntegrationEventService>();

        builder.Services.AddTransient<PatchHistoryUpdatedIntegrationEventHandler>();

        var dotaStatItemsConnectionString = builder.Configuration.GetRequiredConnectionString("DotaStat.Item");

        builder.Services.AddTransient(_ => new DatabaseInitializer(dotaStatItemsConnectionString));
        builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(dotaStatItemsConnectionString));
    }
}
