using Newtonsoft.Json.Linq;
using tech_test.Interface;

namespace tech_test.Logic
{
    public class OnChainBalance : IOnChainBalance
    {
        private static HttpClient _client;
        private static IConfiguration _configuration;

        public OnChainBalance(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;

            var handler = new HttpClientHandler
            {
                UseDefaultCredentials = true 
            };

            _client = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<long> GetOnChainBalance(string address)
        {
            string url = string.IsNullOrWhiteSpace(address)
                ? _configuration["ApiSettings:BalanceUrls:OnChain"].Replace("{address}", _configuration["BitcoinAddresses:ElSalvador"])
                : _configuration["ApiSettings:BalanceUrls:OnChain"].Replace("{address}", address);

            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JObject jsonResponse = JObject.Parse(responseBody);

            long balanceOnChain = (long)jsonResponse["chain_stats"]["funded_txo_sum"] - (long)jsonResponse["chain_stats"]["spent_txo_sum"];

            return balanceOnChain;
        }
    }
}
