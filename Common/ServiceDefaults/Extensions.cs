using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceDefaults;

public static partial class Extensions
{
    //     public static WebApplicationBuilder AddServiceDefaults(this WebApplicationBuilder builder)
    // {
    //     // Shared configuration via key vault
    //     // builder.Configuration.AddKeyVault();
    //
    //     // Shared app insights configuration
    //     // builder.Services.AddApplicationInsights(builder.Configuration);
    //
    //     // builder.Services.AddDefaultOpenApi(builder.Configuration);
    //
    //     // Add the accessor
    //     builder.Services.AddHttpContextAccessor();
    //
    //     return builder;
    // }
    public static IHostApplicationBuilder AddServiceDefaults(this IHostApplicationBuilder builder)
    {
        // todo in future
        // builder.Configuration.AddKeyVault();

        builder.AddDefaultHealthChecks();

        builder.AddEventBus();

        builder.AddDefaultAuthentication();

        builder.AddDefaultOpenApi();

        builder.Services.AddServiceDiscovery();

        builder.Services.ConfigureHttpClientDefaults(
            http =>
            {
                // Turn on resilience by default
                http.AddStandardResilienceHandler();

                // Turn on service discovery by default
                http.AddServiceDiscovery();
            }
        );

        return builder;
    }

    public static WebApplication UseServiceDefaults(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }

        var pathBase = app.Configuration["PATH_BASE"];

        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
            app.UseRouting();

            var identitySection = app.Configuration.GetSection("Identity");

            if (identitySection.Exists())
            {
                // We have to add the auth middleware to the pipeline here
                app.UseAuthentication();
                app.UseAuthorization();
            }
        }

        app.UseDefaultOpenApi();

        app.MapDefaultHealthChecks();

        return app;
    }

    /// <summary>
    /// Adds the services except for making outgoing HTTP calls.
    /// </summary>
    /// <remarks>
    /// This allows for things like Polly to be trimmed out of the app if it isn't used.
    /// </remarks>
    public static IHostApplicationBuilder AddBasicServiceDefaults(this IHostApplicationBuilder builder)
    {
        // Default health checks assume the event bus and self health checks
        builder.AddDefaultHealthChecks();

        // builder.ConfigureOpenTelemetry();

        return builder;
    }

    // todo in future
    // public static ConfigurationManager AddKeyVault(this ConfigurationManager configuration)
    // {
    //     // {
    //     //   "Vault": {
    //     //     "Name": "myvault",
    //     //     "TenantId": "mytenantid",
    //     //     "ClientId": "myclientid",
    //     //    }
    //     // }
    //
    //     var vaultSection = configuration.GetSection("Vault");
    //
    //     if (!vaultSection.Exists())
    //     {
    //         return configuration;
    //     }
    //
    //     var credential = new ClientSecretCredential(
    //         vaultSection.GetRequiredValue("TenantId"),
    //         vaultSection.GetRequiredValue("ClientId"),
    //         vaultSection.GetRequiredValue("ClientSecret"));
    //
    //     var name = vaultSection.GetRequiredValue("Name");
    //
    //     configuration.AddAzureKeyVault(new Uri($"https://{name}.vault.azure.net/"), credential);
    //
    //     return configuration;
    // }
}
