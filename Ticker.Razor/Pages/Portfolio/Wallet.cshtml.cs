using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticker.Razor.Pages.Portfolio
{
    public class WalletModel : PageModel
    {
        public Dictionary<string, string> Wallets { get; set; }

        public WalletModel()
        {

        }

        public void OnGet()
        {
        }
    }
}