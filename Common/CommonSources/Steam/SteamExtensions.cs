using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CommonSources.Steam;

public static class SteamExtensions
{
    public static IHostApplicationBuilder AddSteamHttpClient<T>(this IHostApplicationBuilder builder)
        where T : SteamHttpClientBase
    {
        builder.AddSteamOptions();
        builder.Services.AddHttpClient<T>()
            .AddKey();

        return builder;
    }

    private static IHostApplicationBuilder AddSteamOptions(this IHostApplicationBuilder builder)
    {
        builder.Services.Configure<SteamOptions>(builder.Configuration.GetRequiredSection("Steam"));

        return builder;
    }

    private static IHttpClientBuilder AddKey(this IHttpClientBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddTransient<HttpSteamClientKeyHandler>();

        builder.AddHttpMessageHandler<HttpSteamClientKeyHandler>();

        return builder;
    }

    private class HttpSteamClientKeyHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SteamOptions _steamOptions;

        public HttpSteamClientKeyHandler(
            IHttpContextAccessor httpContextAccessor,
            IOptions<SteamOptions> steamOptions
            //, HttpMessageHandler innerHandler
        )// : base(innerHandler)
        {
            _httpContextAccessor = httpContextAccessor;
            _steamOptions = steamOptions.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_httpContextAccessor.HttpContext is { } context)
            {
                context.Request.QueryString.Add(nameof(_steamOptions.Key), _steamOptions.Key);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
