using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticker.Razor.Filters;

namespace Ticker.Razor.Infrastructure
{
    [TRAuthorize]
    public class RTPageModel : PageModel
    {

    }
}
