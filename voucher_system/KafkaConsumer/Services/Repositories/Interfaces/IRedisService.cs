using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repositories.Interfaces
{
    public interface IRedisService
    {
        public Task<bool> Save(string _key, string _json);
    }
}
