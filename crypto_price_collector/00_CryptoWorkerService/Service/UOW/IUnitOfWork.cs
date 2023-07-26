using _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.UOW
{
    public interface IUnitOfWork
    {
        public IBitcoinPriceProducer BitcoinPriceProducer { get; }
    }
}
