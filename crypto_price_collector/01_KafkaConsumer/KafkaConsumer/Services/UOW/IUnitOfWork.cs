using KafkaConsumer.Services.UOW.bitcoin_price_consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.UOW
{
    public interface IUnitOfWork
    {
        public IBitcoinPriceConsumer BitcoinPriceConsumer { get; }
    }
}
