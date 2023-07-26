using _00_CryptoWorkerService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _00_CryptoWorkerService.Service.Repo.Interface
{
    public interface IKafkaService
    {
        public Task<bool> ProduceMessage(MCryptoData _mCryptoData);
    }
}
