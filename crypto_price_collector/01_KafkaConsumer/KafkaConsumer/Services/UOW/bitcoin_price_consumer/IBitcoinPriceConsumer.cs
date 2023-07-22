using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.UOW.bitcoin_price_consumer
{
    public interface IBitcoinPriceConsumer
    {
        public Task<bool> get_price_from_kafka_topic(string kafkaTopic);
    }
}
