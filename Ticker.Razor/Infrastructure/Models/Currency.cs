using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticker.Razor.Infrastructure.Models
{
    public class Currency
    {
        public Guid UUID { get; set; }

        public static IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>() { };
        }
    }
}
