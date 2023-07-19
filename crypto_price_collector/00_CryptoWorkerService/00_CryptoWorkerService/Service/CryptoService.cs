using _00_CryptoWorkerService.Helper;
using _00_CryptoWorkerService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service
{
    public class CryptoService
    {
        public async Task<MCryptoData> GetPriceData(IHttpClientFactory _httpClientFactory, string _tokenIds)
        {
            MCryptoData mCryptoData = null;
            try
            {
                string _query = $"https://api.coingecko.com/api/v3/coins/{_tokenIds}";
                // Create HttpRequestMessage and set headers
                var request = new HttpRequestMessage(HttpMethod.Get, _query);

                // Send the request using HttpClient
                var httpClient = _httpClientFactory.CreateClient("GitHub");
                var response = await httpClient.SendAsync(request);

                // Process the response
                if (response.IsSuccessStatusCode)
                {
                    // Handle successful response
                    var content = await response.Content.ReadAsStringAsync();
                    // Process the content
                    if (string.IsNullOrEmpty(content))
                    {
                        mCryptoData = null;
                    }
                    else if (JsonHelper.IsValidJson(content))
                    {
                        mCryptoData = new MCryptoData();
                        mCryptoData = JsonConvert.DeserializeObject<MCryptoData>(content);
                    }
                    else
                    {
                        mCryptoData = null;
                    }
                }
                else
                {
                    // Handle error response
                    // Log or throw appropriate exceptions
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle network-related errors
                // Log or throw appropriate exceptions

                mCryptoData = null;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or throw appropriate exceptions

                mCryptoData = null;
            }

            return mCryptoData;
        }
    }
}
