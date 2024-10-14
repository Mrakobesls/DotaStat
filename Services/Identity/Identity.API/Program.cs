using Identity.API.Configuration;
using Identity.API.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// builder.AddServiceDefaults();
builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .Build();
    });
});

builder.Services.AddControllersWithViews();


// builder.AddNpgsqlDbContext<ApplicationDbContext>("IdentityDB");

// Apply database migration automatically. Note that this approach is not
// recommended for production scenarios. Consider generating SQL scripts from
// migrations instead.
// builder.Services.AddMigration<ApplicationDbContext, UsersSeed>();

// builder.Services.AddIdentity<Microsoft.AspNetCore.Identity.IdentityUser, Microsoft.AspNetCore.Identity.IdentityRole>()
//         // .AddEntityFrameworkStores<ApplicationDbContext>()
//         .AddDefaultTokenProviders();
// builder.Services.AddOidcStateDataFormatterCache();



builder.Services.AddIdentityServer(options =>
{
    options.IssuerUri = "null";
    options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
.AddInMemoryIdentityResources(Config.GetResources())
.AddInMemoryApiScopes(Config.GetApiScopes())
.AddInMemoryApiResources(Config.GetApis())
.AddInMemoryClients(Config.GetClients(builder.Configuration))
.AddDeveloperSigningCredential(); // Not recommended for production - you need to store your key material somewhere secure


builder.Services.AddAuthentication()
    .AddSteam(
        options =>
        {
            options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

            options.CorrelationCookie.SameSite = SameSiteMode.None;
            options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.None;
        }
    );

var app = builder.Build();

// app.MapDefaultEndpoints();

app.UseStaticFiles();

app.UseCors("default");
// This cookie policy fixes login issues with Chrome 80+ using HTTP
app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.MapGet("textOk", () => "Ok");

app.Run();
