using StackExchange.Redis;
using System.Threading.Channels;
using web_api_1.Services.Interfaces;

namespace web_api_1.Services
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly ISubscriber _redisSubscriber;

        public RedisService(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection;
            _redisSubscriber = _redisConnection.GetSubscriber(); ;
        }

        public async Task<string> GetValueAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task<bool> SetValueAsync(string key, string value)
        {
            try
            {
                var db = _redisConnection.GetDatabase();

                await db.StringSetAsync(key, value);
                await _redisSubscriber.PublishAsync(key, value, CommandFlags.FireAndForget);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
