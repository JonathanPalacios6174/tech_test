using Newtonsoft.Json;
using System.Transactions;
using Newtonsoft.Json.Linq;
using tech_test.Interface;
using tech_test.Models;
namespace tech_test.Logic
{
    public class BalanceVariation : IBalanceVariation
    {
        private static HttpClient _client; //Abre el internet
        private static IConfiguration _configuration; // Trae las configuraciones de los endpoints
        public BalanceVariation(IConfiguration configuration, HttpClient client)
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
        public async Task<(long balance30DaysAgo, long balance7DaysAgo)> BalanceVariations(string address)
        {
            string url = string.IsNullOrWhiteSpace(address)
                ? _configuration["ApiSettings:BalanceUrls:Balance"].Replace("{address}", _configuration["BitcoinAddresses:ElSalvador"])
                : _configuration["ApiSettings:BalanceUrls:Balance"].Replace("{address}", address);

            long thirtyDaysAgoUnix = DateTimeOffset.UtcNow.AddDays(-30).ToUnixTimeSeconds();
            long sevenDaysAgoUnix = DateTimeOffset.UtcNow.AddDays(-7).ToUnixTimeSeconds();
            long balance30DaysAgo = 0;
            long balance7DaysAgo = 0;

            using (var client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                List<Models.MempoolBalance> transactions = JsonConvert.DeserializeObject<List<Models.MempoolBalance>>(response);

                foreach (var transaction in transactions)
                {
                    if (transaction.status?.confirmed == true)
                    {
                        foreach (var vout in transaction.vout)
                        {
                            if (vout?.prevout?.scriptpubkey_address == address)
                            {
                                long transactionTime = transaction.locktime;

                                if (transactionTime >= sevenDaysAgoUnix)
                                {
                                    balance7DaysAgo += vout.prevout?.value ?? 0;
                                }

                                if (transactionTime >= thirtyDaysAgoUnix)
                                {
                                    balance30DaysAgo += vout.prevout?.value ?? 0;
                                }
                            }
                        }
                    }
                }
            }

            return (balance30DaysAgo, balance7DaysAgo);
        }





    }
}
