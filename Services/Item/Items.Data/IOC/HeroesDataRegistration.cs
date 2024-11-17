using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceDefaults;

namespace Items.Data.IOC;

public static class HeroesDataRegistration
{
    public static void RegisterPatchData(this IHostApplicationBuilder builder)
    {
        var dotaStatHeroesConnectionString = builder.Configuration.GetRequiredConnectionString("DotaStat.Heroes");
        builder.Services.AddDbContext<ItemDbContext>(
            options => options.UseSqlServer(dotaStatHeroesConnectionString)
        );
    }
}
