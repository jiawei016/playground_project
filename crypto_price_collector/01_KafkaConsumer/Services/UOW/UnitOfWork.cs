using KafkaConsumer.Services.Repos.Interfaces;
using KafkaConsumer.Services.UOW.bitcoin_price_consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBitcoinPriceConsumer _IBitcoinPriceConsumer;
        private readonly IServiceProvider _IServiceProvider;

        public UnitOfWork(IServiceProvider serviceProvider, IKafkaService _IKafkaService)
        {
            _IServiceProvider = serviceProvider;
        }
        public IBitcoinPriceConsumer BitcoinPriceConsumer => _IBitcoinPriceConsumer ??= new BitcoinPriceConsumer(_IServiceProvider);
    }
}
