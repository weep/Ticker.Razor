using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Ticker.Razor.Pages.Portfolio
{
    public class IndexModel : PageModel
    {
        private readonly ISession _session;

        public string ErrorMessage { get; set; }
        public bool HasErrorMessage => !string.IsNullOrWhiteSpace(ErrorMessage);

        public string Seed { get; set; }
        public bool LoggedIn => _session.Keys.Contains("seed");

        public IndexModel(IHttpContextAccessor httpContextAccessor)
        {
            _session = httpContextAccessor.HttpContext.Session;
        }

        public void OnGet()
        {
            Seed = _session.GetString("seed");
        }

        public IActionResult OnPost(string seed)
        {
            if (Regex.IsMatch(seed ?? string.Empty, @"[A-Fa-f0-9]{64}"))
            {
                _session.SetString("seed", seed);
                return RedirectToPage("/Portfolio/Wallet");
            }

            ErrorMessage = "Seed does not match pattern [A-Fa-f0-9]{64}";
            return Page();
        }


        public IActionResult OnPostLogout()
        {
            _session.Remove("seed");
            return RedirectToPage();
        }

        public void OnPostNew()
        {
            Seed = "7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069";
        }

    }
}