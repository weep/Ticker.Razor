using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ticker.Razor.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; private set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Identifier { get; set; }

            [Required]
            [RegularExpression("[A-Fa-f0-9]{64}")]
            public string Seed { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // await GenerateIdentityAndSeedAsync();

            ReturnUrl = returnUrl;

        }

        public async Task<IActionResult> OnPostLoginAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                return await LoginAsync();
            }

            return Page();
        }

        private async Task<IActionResult> LoginAsync()
        {
            var authorized = AuthentiaceUser();

            var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, Input.Identifier),
                    new Claim("Seed", Input.Seed)
                };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var p = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, p);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Account/Login");
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            await GenerateIdentityAndSeedAsync();
            return await LoginAsync();
        }

        private async Task GenerateIdentityAndSeedAsync()
        {
            if (Input == null)
                Input = new InputModel();

            //Generate id
            var sha256 = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(Guid.NewGuid().ToString()));
            Input.Identifier = await Task.FromResult(sha256.ToList().Aggregate("", (acc, x) => acc + x.ToString("X2")));
            Input.Identifier = Input.Identifier.Substring(0, 24);


            //Generate seed
            sha256 = SHA256.Create().ComputeHash(Encoding.Unicode.GetBytes(Guid.NewGuid().ToString()));
            Input.Seed = await Task.FromResult(sha256.ToList().Aggregate("", (acc, x) => acc + x.ToString("X2")));
        }

        private async Task<bool> AuthentiaceUser()
        {
            await Task.Delay(5000);

            if (string.IsNullOrWhiteSpace(Input.Identifier))
                return false;
            if (string.IsNullOrWhiteSpace(Input.Seed))
                return false;

            return true;
        }
    }
}