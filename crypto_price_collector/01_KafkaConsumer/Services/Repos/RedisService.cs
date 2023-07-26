using KafkaConsumer.Services.Repos.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.Services.Repos
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

            bool status = db.StringSet(_key, _json);
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> Get(string _key)
        {
            IDatabase db = _redisConnection.GetDatabase();

            string _value = db.StringGet(_key);
            if (string.IsNullOrEmpty(_value))
            {
                return null;
            }
            else
            {
                return _value;
            }
        }
    }
}
