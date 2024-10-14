using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ServiceDefaults;

public static partial class Extensions
{
    public static IServiceCollection AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // {
        //   "Identity": {
        //     "Url": "http://identity",
        //     "Audience": "basket"
        //    }
        // }

        var identitySection = configuration.GetSection("Identity");

        if (!identitySection.Exists())
        {
            // No identity section, so no authentication
            return services;
        }

        // prevent from mapping "sub" claim to nameidentifier.
        // JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub"); // todo

        services.AddAuthentication()
            .AddJwtBearer(
                options =>
                {
                    var identityUrl = identitySection.GetRequiredValue("Url");
                    var audience = identitySection.GetRequiredValue("Audience");

                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;
                    options.Audience = audience;
                    options.TokenValidationParameters.ValidateAudience = false;
                }
            );

        services.AddAuthorization();

        return services;
    }
}
