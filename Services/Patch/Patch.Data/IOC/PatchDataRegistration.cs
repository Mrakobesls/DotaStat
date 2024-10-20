using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patch.Data.Repository;
using ServiceDefaults;

namespace Patch.Data.IOC;

public static class PatchDataRegistration
{
    public static void RegisterPatchData(this IHostApplicationBuilder builder)
    {
        var dotaStatPatchConnectionString = builder.Configuration.GetRequiredConnectionString("DotaStat.Patch");

        builder.Services.AddTransient(_ => new DatabaseInitializer(dotaStatPatchConnectionString));
        builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(dotaStatPatchConnectionString));

        builder.Services.AddScoped<IDataPatchCommands, DataPatchCommands>();
        builder.Services.AddScoped<IDataPatchQueries, DataPatchQueries>();
    }
}
