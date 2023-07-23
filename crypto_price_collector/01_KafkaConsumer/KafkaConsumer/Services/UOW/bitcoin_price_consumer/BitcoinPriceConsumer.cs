using KafkaConsumer.Services.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.UOW.bitcoin_price_consumer
{
    public class BitcoinPriceConsumer : IBitcoinPriceConsumer
    {
        private readonly IServiceProvider _IServiceProvider;
        private readonly IKafkaService _IKafkaService;
        private readonly IRedisService _IRedisService;
        public BitcoinPriceConsumer(IServiceProvider serviceProvider) 
        {
            _IServiceProvider = serviceProvider;
            _IKafkaService = serviceProvider.GetRequiredService<IKafkaService>();
            _IRedisService = serviceProvider.GetRequiredService<IRedisService>();
        }

        public async Task<bool> get_price_from_kafka_topic(string kafkaTopic)
        {
            try
            {
                await _IKafkaService.ConsumeTopic(kafkaTopic);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
