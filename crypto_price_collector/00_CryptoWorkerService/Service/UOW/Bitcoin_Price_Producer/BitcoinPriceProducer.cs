using _00_CryptoWorkerService.Models;
using _00_CryptoWorkerService.Service.Repo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer
{
    public class BitcoinPriceProducer : IBitcoinPriceProducer
    {
        private readonly ICryptoService _ICryptoService;
        private readonly IKafkaService _IKafkaService;
        public BitcoinPriceProducer(IServiceProvider serviceProvider) 
        {
            _ICryptoService = serviceProvider.GetRequiredService<ICryptoService>();
            _IKafkaService = serviceProvider.GetRequiredService<IKafkaService>();
        }

        public async Task<bool> get_price_and_producemessage(string cryptoSymbol)
        {
            try
            {
                MCryptoData mCryptoData = await _ICryptoService.GetPriceData(cryptoSymbol);

                if (mCryptoData == null)
                {
                    return false;
                }

                bool status = await _IKafkaService.ProduceMessage(mCryptoData);

                Console.WriteLine($"Published to kafka: {status}");
                if (status)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
