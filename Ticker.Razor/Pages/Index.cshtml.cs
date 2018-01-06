using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ticker.Razor.Pages
{
    public class IndexModel : PageModel
    {

        public IActionResult OnGet()
        {
            return RedirectToPage("Markets/Index");
        }
    }
}