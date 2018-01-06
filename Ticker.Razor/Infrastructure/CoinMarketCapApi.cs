using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ticker.Razor.Infrastructure.Models;

namespace Ticker.Razor.Infrastructure
{
    public class CoinMarketCapApi : MarketApiBase, IMarketApi
    {
        public CoinMarketCapApi(HttpClient httpClient) : base(httpClient)
        {
            BaseUrl = "https://api.coinmarketcap.com/v1/ticker/";
        }

        public IEnumerable<Currency> GetCurrencies()
        {
            return new List<Currency>
            {
                new Currency{ UUID = Guid.NewGuid() }
            };
        }
    }
}
