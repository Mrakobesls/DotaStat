using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PatchChecker.Data.Repository;

namespace PatchChecker.Data.IOC;

public static class PatchDataRegistration
{
    public static void Register(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
        builder.Services.AddScoped<IDataPatchCommands, DataPatchCommands>();
        builder.Services.AddScoped<IDataPatchQueries, DataPatchQueries>();
    }
}
