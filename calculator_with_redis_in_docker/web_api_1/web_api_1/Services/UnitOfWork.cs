using StackExchange.Redis;
using web_api_1.Services.Interfaces;

namespace web_api_1.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IExpressService _expressService;
        private readonly IRedisService _redisService;
        private readonly ICassandraService _cassandraService;

        public UnitOfWork(IRedisService redisService, ICassandraService mongoService)
        {
            _redisService = redisService;
            _cassandraService = mongoService;
        }

        public IExpressService ExpressService => _expressService ??= new ExpressService(_redisService, _cassandraService);

        public async Task CommitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
