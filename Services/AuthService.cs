using CookieAuthLesson.DataAccess;
using CookieAuthLesson.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookieAuthLesson.Services
{
    public class AuthService
    {
        private readonly AuthContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthService(IHttpContextAccessor httpContextAccessor, AuthContext context)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<bool> AuthenticateUser(string email, string password)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email && x.Password == password);

            if(user == null)
            {
                return false;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = false,

                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),

                RedirectUri = "/Home/Index"
            };

            await httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return true;
        }
    }
}
