using CommonSources.Steam;
using EventBus.Abstractions;
using Hero.Business.IntegrationEvents;
using Hero.Business.IntegrationEvents.EventHandlers;
using Hero.Business.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hero.Business.IOC;

public static class HeroBusinessRegistration
{
    public static void RegisterHeroBusiness(this IHostApplicationBuilder builder)
    {
        builder.AddSteamHttpClient<SteamHttpClient>();

        // builder.Services.AddScoped<IHeroCommands, HeroCommands>();
        //
        // builder.Services.AddScoped<IIntegrationEventService, HeroIntegrationEventService>();
        //
        // builder.Services.AddTransient<PingCalledIntegrationEventHandler>();
        // builder.Services.AddTransient<PatchHistoryUpdatedIntegrationEventHandler>();
    }
}
