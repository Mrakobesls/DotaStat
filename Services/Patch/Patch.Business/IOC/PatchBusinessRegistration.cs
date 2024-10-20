using CommonSources.OpenDota;
using EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Patch.Business.IntegrationEvents;
using Patch.Business.Services;

namespace Patch.Business.IOC;

public static class PatchBusinessRegistration
{
    public static void RegisterPatchBusiness(this IHostApplicationBuilder builder)
    {
        builder.AddOpenDotaHttpClient<OpenDotaHttpClient>();
        builder.Services.AddScoped<IPatchQueries, PatchQueries>();
        builder.Services.AddScoped<IPatchService, PatchService>();

        builder.Services.AddScoped<IIntegrationEventService, PatchIntegrationEventService>();
    }
}
