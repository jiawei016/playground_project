using _00_CryptoWorkerService.Service.Repo.Interface;
using _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBitcoinPriceProducer _BitcoinPriceProducer;
        private readonly IServiceProvider _IServiceProvider;

        public UnitOfWork(IServiceProvider serviceProvider, IKafkaService _IKafkaService)
        {
            _IServiceProvider = serviceProvider;
        }
        public IBitcoinPriceProducer BitcoinPriceProducer => _BitcoinPriceProducer ??= new BitcoinPriceProducer(_IServiceProvider);
    }
}
