using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatRoom.Controlers;

public class AccountController : Controller
{
    public IActionResult Login(string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel user, string? returnurl = "/")
    {
        if ((user.UserName == "sajad" || user.UserName == "sara") && user.Password == "sara136739")
        {


            // لیست  claim
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        //new Claim("FullName", user.FullName),
        new Claim(ClaimTypes.Role, "Administrator"),
    };

            // claimsIdentity

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);


            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //   مرا بخاطر بسپار

                IsPersistent = false,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };


            //    این خودش باعث میشه بره کوکی رو بسازه
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);



            return Redirect(returnurl);
        }
        else
            return View();
    }

    public async Task<IActionResult> LogOut(string returnUrl = "/")
    {
        await HttpContext.SignOutAsync(
     CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect(returnUrl);
    }

}


public class LoginViewModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
}