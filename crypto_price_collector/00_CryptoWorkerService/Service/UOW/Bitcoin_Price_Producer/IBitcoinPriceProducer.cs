using _00_CryptoWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer
{
    public interface IBitcoinPriceProducer
    {
        public Task<bool> get_price_and_producemessage(string cryptoSymbol);
    }
}
