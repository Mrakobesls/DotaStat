using CommonSources.Steam;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDefaults;
using Statistics.Business.Services;

namespace Statistics.Business.IOC;

public static class ServicesRegistration
{
    public static void RegisterStatistics(this IHostApplicationBuilder builder)
    {
        builder.AddSteamHttpClient<SteamHttpClient>();

        builder.Services.AddDbContext<DotaStatDbContext>(
            options => options.UseSqlServer(
                builder.Configuration.GetRequiredConnectionString("DotaStat.Statistics")
            )
        );
        builder.Services.AddScoped<IHeroStatisticsService, HeroStatisticsService>();
        builder.Services.AddScoped<IWeekPatchService, WeekPatchService>();
        builder.Services.AddScoped<IHeroService, HeroService>();
    }
}
