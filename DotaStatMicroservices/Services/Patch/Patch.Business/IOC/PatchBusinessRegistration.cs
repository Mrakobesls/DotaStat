using CommonSources.OpenDota;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patch.Business.Services;

namespace Patch.Business.IOC;

public static class PatchBusinessRegistration
{
    public static void RegisterPatchBusiness(this IHostApplicationBuilder builder)
    {
        builder.AddOpenDotaHttpClient<OpenDotaHttpClient>();
        builder.Services.AddTransient<IPatchQueries, PatchQueries>();
        builder.Services.AddTransient<IPatchService, PatchService>();
    }
}
