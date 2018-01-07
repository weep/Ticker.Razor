using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Ticker.Razor.Infrastructure;
using Ticker.Razor.Infrastructure.Models;

namespace Ticker.Razor.Pages.Markets
{
    public class IndexModel : RTPageModel
    {
        private readonly ISession _session;
        private readonly IMarketApi _api;

        public string SessionId { get; set; }
        public string Name { get; set; }

        public IEnumerable<Currency> Currencies;

        public IndexModel(IHttpContextAccessor httpContextAccessor, IMarketApi api)
        {
            _session = httpContextAccessor.HttpContext.Session;
            _api = api;
            Currencies = _api.GetCurrencies();
        }

        public void OnGet()
        {
            SessionId = _session.Id;
            Name = _session.GetString("name");
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = SessionId;
                _session.SetString("name", Name);
            }
        }

        public async void OnPostAsync()
        {
            await Task.FromResult("Swag");
            Name = "Swag";
            //return RedirectToPage();
        }
    }
}