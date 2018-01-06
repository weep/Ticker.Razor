using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ticker.Razor.Infrastructure
{
    public abstract class MarketApiBase
    {
        private readonly HttpClient _client;

        public string BaseUrl { get; protected set; }

        public MarketApiBase(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<TResp> GetAsync<TResp>(string request)
        {
            string content = await _client.GetStringAsync(request);

            return JsonConvert.DeserializeObject<TResp>(content);
        }
    }
}
