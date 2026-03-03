using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Transcard.Application.DTOs;

namespace Transcard.WebApp.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        if (request.Username != "admin" || request.Password != "admin")
            return Redirect("/login?error=true");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, request.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return Redirect("/paymentform");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
    //await HttpContext.SignOutAsync();
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Redirect("/Login");
        //return Ok();
    }
}
