using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticker.Razor.Infrastructure.Models;

namespace Ticker.Razor.Infrastructure
{
    public interface IMarketApi
    {
        IEnumerable<Currency> GetCurrencies();
    }
}
