using Microsoft.AspNetCore.Authentication.Cookies;
using Transcard.Application.Interfaces;
using Transcard.Application.Services;
using Transcard.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//
// =========================
// APPLICATION SERVICES
// =========================
//

// Dependency Injection for Clean Architecture
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

//
// =========================
// AUTHENTICATION
// =========================
//

// Registers cookie authentication handler
builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Transcard.Auth";
        options.Cookie.HttpOnly = true; // Prevent JavaScript access
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
        options.Cookie.SameSite = SameSiteMode.Strict; // CSRF protection

        options.LoginPath = "/api/auth/unauthorized";
        options.AccessDeniedPath = "/api/auth/forbidden";

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

//
// =========================
// CONTROLLERS & SWAGGER
// =========================
//

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//
// =========================
// MIDDLEWARE PIPELINE
// =========================
//

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication MUST come before Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();