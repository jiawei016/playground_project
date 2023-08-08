using KafkaConsumer.Services.Repositories.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repositories
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        public RedisService(IConfiguration configuration)
        {
            _redisConnection = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value);
        }

        public async Task<bool> Save(string _key, string _json)
        {
            IDatabase db = _redisConnection.GetDatabase();

            bool status = await db.StringSetAsync(_key, _json);
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
