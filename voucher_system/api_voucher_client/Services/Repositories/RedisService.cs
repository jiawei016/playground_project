using api_voucher_client.Services.Repositories.Interfaces;
using StackExchange.Redis;

namespace api_voucher_client.Services.Repositories
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _redisConnection;
        public RedisService(IConfiguration configuration)
        {
            _redisConnection = ConnectionMultiplexer.Connect(configuration.GetSection("RedisConfiguration").GetSection("ConnectionString").Value);
        }

        public async Task<string> GetRedisValue(string _key)
        {
            IDatabase db = _redisConnection.GetDatabase();

            string _value = await db.StringGetDeleteAsync(_key);
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
