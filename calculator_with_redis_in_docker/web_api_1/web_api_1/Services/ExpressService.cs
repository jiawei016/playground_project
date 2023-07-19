using Microsoft.Extensions.Options;
using StackExchange.Redis;
using web_api_1.Contexts;
using web_api_1.Models;
using web_api_1.Services.Interfaces;
using web_api_1.Settings;

namespace web_api_1.Services
{
    public class ExpressService : IExpressService
    {
        private readonly IRedisService _redisService;
        private readonly ICassandraService _cassandraService;

        public ExpressService(IRedisService redisService, ICassandraService cassandraService)
        {
            _redisService = redisService;
            _cassandraService = cassandraService;
        }
        public async Task<string> GetValueAsync(string key)
        {
            string _value = await _redisService.GetValueAsync(key);

            return _value;
        }

        public async Task<bool> SetValueAsync(UContentModel _UContentModel)
        {
            bool _status_1 = await _redisService.SetValueAsync(_UContentModel._URedis_Content.redis_id, _UContentModel._URedis_Content.redis_value);
            bool _status_2 = await _cassandraService.ExecuteAsync(_UContentModel._UCassandra_Content.query);

            if (_status_1 == true && _status_2 == true)
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
