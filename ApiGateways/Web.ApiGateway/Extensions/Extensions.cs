// using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Components.Authorization;
// using Microsoft.AspNetCore.Components.Server;
// using Microsoft.IdentityModel.JsonWebTokens;
//
// namespace Web.ApiGateway.Extensions;
//
// public static class Extensions
// {
//     public static void AddApplicationServices(this IHostApplicationBuilder builder)
//     {
//         builder.AddAuthenticationServices();
//
//         // builder.AddRabbitMqEventBus("EventBus")
//         //     .AddEventBusSubscriptions();
//         //
//         // builder.Services.AddHttpForwarderWithServiceDiscovery();
//
//         // Application services
//
//         // HTTP and GRPC client registrations
//         // builder.Services.AddGrpcClient<Basket.BasketClient>(o => o.Address = new("http://basket-api"))
//         //     .AddAuthToken();
//     }
//
//     public static void AddAuthenticationServices(this IHostApplicationBuilder builder)
//     {
//         var configuration = builder.Configuration;
//         var services = builder.Services;
//
//         JsonWebTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
//
//         var identityUrl = configuration.GetRequiredValue("IdentityUrl");
//         var callBackUrl = configuration.GetRequiredValue("CallBackUrl");
//         var sessionCookieLifetime = configuration.GetValue("SessionCookieLifetimeMinutes", 60);
//
//         // Add Authentication services
//         services.AddAuthorization();
//         services.AddAuthentication(options =>
//             {
//                 options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                 options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//             })
//             .AddCookie(options => options.ExpireTimeSpan = TimeSpan.FromMinutes(sessionCookieLifetime))
//             .AddOpenIdConnect(options =>
//             {
//                 options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//                 options.Authority = identityUrl;
//                 options.SignedOutRedirectUri = callBackUrl;
//                 options.ClientId = "webapp";
//                 options.ClientSecret = "secret";
//                 options.ResponseType = "code";
//                 options.SaveTokens = true;
//                 options.GetClaimsFromUserInfoEndpoint = true;
//                 options.RequireHttpsMetadata = false;
//                 options.Scope.Add("openid");
//                 options.Scope.Add("profile");
//                 options.Scope.Add("orders");
//                 options.Scope.Add("basket");
//             });
//
//         // Blazor auth services
//         services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
//         services.AddCascadingAuthenticationState();
//     }
// }
