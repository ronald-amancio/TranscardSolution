using Microsoft.AspNetCore.Authentication.Cookies;
using Transcard.WebApp.Components;
using Transcard.WebApp.Services;

using Transcard.Application.Interfaces;
using Transcard.Application.Services;
using Transcard.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//
// ==============================
// HTTP CLIENT (API CALLS)
// ==============================
//

var apiUrl = builder.Configuration["ApiBaseUrl"];

builder.Services.AddHttpClient<IPaymentClient, PaymentClient>(client =>
{
    client.BaseAddress = new Uri(apiUrl!);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        UseCookies = true
    };
});

//
// ==============================
// AUTHENTICATION & AUTHORIZATION
// ==============================
//

// Registers authentication services (required for AuthorizeRouteView)
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        //options.LoginPath = "/login";          
        //options.AccessDeniedPath = "/login";  

        options.Cookie.Name = "Transcard.Auth";
        options.Cookie.HttpOnly = true; // Prevent JavaScript access
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
        options.Cookie.SameSite = SameSiteMode.Strict; // CSRF protection

        options.LoginPath = "/login";
        options.AccessDeniedPath = "/api/auth/forbidden";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(2);
        options.SlidingExpiration = true;
    });

// Enables [Authorize] and policy system
builder.Services.AddAuthorization();

// Required for Blazor authentication state propagation
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<LoadingService>();

builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//
// ==============================
// RAZOR COMPONENTS
// ==============================
//

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
//.AddInteractiveServerComponents(options =>
//{
//    options.DisablePrerendering = true;
//});

builder.Services.AddControllers();

var app = builder.Build();

//
// ==============================
// MIDDLEWARE PIPELINE
// ==============================
//

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// These MUST come before MapRazorComponents
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers(); //For routing controllers like AuthController

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();