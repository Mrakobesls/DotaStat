using Hero.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDefaults;

namespace Hero.Data.IOC;

public static class HeroDataRegistration
{
    public static void RegisterHeroData(this IHostApplicationBuilder builder)
    {
        var dotaStatHeroesConnectionString = builder.Configuration.GetRequiredConnectionString("DotaStat.Hero");
        builder.Services.AddDbContext<HeroesDbContext>(
            options => options.UseSqlServer(dotaStatHeroesConnectionString)
        );

        builder.Services.AddScoped<IDataHeroCommands, DataHeroCommands>();
        builder.Services.AddScoped<IDataHeroQueries, DataHeroQueries>();
    }
}
