using _00_CryptoWorkerService.Helper;
using _00_CryptoWorkerService.Models;
using _00_CryptoWorkerService.Service.Repo.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.Repo
{
    public class CryptoService : ICryptoService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CryptoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<MCryptoData> GetPriceData(string _tokenIds)
        {
            MCryptoData mCryptoData = null;
            try
            {
                //string _query = $"https://api.coingecko.com/api/v3/coins/{_tokenIds}";

                //var request = new HttpRequestMessage(HttpMethod.Get, _query);

                //var _httpClient = _httpClientFactory.CreateClient("http_client");

                //var response = _httpClient.GetAsync(_query).Result;

                //if (response.IsSuccessStatusCode)
                //{
                //    var content = await response.Content.ReadAsStringAsync();

                //    if (string.IsNullOrEmpty(content))
                //    {
                //        mCryptoData = null;
                //    }
                //    else if (content.IsValidJson())
                //    {
                //        mCryptoData = new MCryptoData();
                //        mCryptoData = JsonConvert.DeserializeObject<MCryptoData>(content);
                //        Console.WriteLine($"Current Price MYR : {mCryptoData.market_data.current_price.myr}");
                //    }
                //    else
                //    {
                //        mCryptoData = null;
                //    }
                //}
                //else
                //{
                //    mCryptoData = null;
                //}
                mCryptoData = new MCryptoData
                {
                    id = _tokenIds,
                    symbol = "ABC",
                    name = "Bitcoin",
                    market_data = new MCryptoData._market_data
                    {
                        current_price = new MCryptoData._current_price
                        {
                            myr = 30000
                        }
                    }
                };
                Console.WriteLine($"Current Price MYR : {mCryptoData.market_data.current_price.myr}");
            }
            catch (Exception ex)
            {
                mCryptoData = null;
            }

            return mCryptoData;
        }
    }
}
