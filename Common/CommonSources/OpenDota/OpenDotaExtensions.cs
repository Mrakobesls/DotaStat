using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommonSources.OpenDota;

public static class OpenDotaExtensions
{
    public static IHostApplicationBuilder AddOpenDotaHttpClient<T>(this IHostApplicationBuilder builder)
        where T : OpenDotaHttpClientBase
    {
        builder.Services.AddHttpClient<T>();

        return builder;
    }
}
