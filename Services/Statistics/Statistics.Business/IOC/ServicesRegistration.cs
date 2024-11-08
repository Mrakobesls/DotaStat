using CommonSources.Steam;
using EventBus.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDefaults;
using Statistics.Business.Infrastructure;
using Statistics.Business.IntegrationEvents;
using Statistics.Business.Services;

namespace Statistics.Business.IOC;

public static class ServicesRegistration
{
    public static void RegisterStatisticsBusiness(this IHostApplicationBuilder builder)
    {
        builder.AddSteamHttpClient<SteamHttpClient>();

        builder.Services.AddScoped<IIntegrationEventService, StatisticsIntegrationEventService>();

        builder.Services.AddDbContext<StatisticsDbContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetRequiredConnectionString("DotaStat.Statistics")
            )
        );
        builder.Services.AddScoped<IHeroStatisticsService, HeroStatisticsService>();
        builder.Services.AddScoped<IWeekPatchService, WeekPatchService>();
        builder.Services.AddScoped<IHeroService, HeroQueries>();
    }
}
