using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Statistics.Business.Services;

namespace Statistics.Business.IOC;

public static class ServicesRegistration
{
    public static void RegisterStatistics(this IServiceCollection services, string connectionString)
    {
        services.AddHttpClient<SteamHttpClient>();
        services.AddDbContext<DotaStatDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IHeroStatisticsService, HeroStatisticsService>();
        services.AddScoped<IWeekPatchService, WeekPatchService>();
        services.AddScoped<IHeroService, HeroService>();
    }
}
