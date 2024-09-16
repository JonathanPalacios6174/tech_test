using Newtonsoft.Json.Linq;
using tech_test.Interface;

namespace tech_test.Logic
{
    public class MempoolBalance : IMempoolBalances
    {
        private static HttpClient _client;
        private static IConfiguration _configuration;
        public MempoolBalance(IConfiguration configuration, HttpClient client)
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

        public async Task<long> MempoolBalances(string address)
        {
            string url = string.IsNullOrWhiteSpace(address)
            ? _configuration["ApiSettings:BalanceUrls:OnChain"].Replace("{address}", _configuration["BitcoinAddresses:ElSalvador"])
            : _configuration["ApiSettings:BalanceUrls:OnChain"].Replace("{address}", address);


            HttpResponseMessage response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            JToken jsonToken = JToken.Parse(responseBody);
            long mempoolBalance = 0;

            if (jsonToken.Type == JTokenType.Array)
            {
                foreach (var item in jsonToken.Children<JObject>())
                {
                    mempoolBalance += (long)item["value"];
                }
            }
            else if (jsonToken.Type == JTokenType.Object)
            {
                var jsonObject = (JObject)jsonToken;
                var mempoolStats = (JObject)jsonObject["mempool_stats"];
                mempoolBalance = (long)mempoolStats["funded_txo_sum"];
            }
            else
            {
                throw new Exception("Unexpected JSON format");
            }

            return mempoolBalance;
        }
    }
}
