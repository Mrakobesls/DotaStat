using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patch.Data.Repository;
using ServiceDefaults;

namespace Patch.Data.IOC;

public static class PatchDataRegistration
{
    public static void RegisterPatchData(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient(_ => new DatabaseInitializer(builder.Configuration.GetRequiredConnectionString("DotaStat.Patch")));
        builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        builder.Services.AddScoped<IDataPatchCommands, DataPatchCommands>();
        builder.Services.AddScoped<IDataPatchQueries, DataPatchQueries>();
    }
}
