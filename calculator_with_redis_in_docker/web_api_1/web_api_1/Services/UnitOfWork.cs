using StackExchange.Redis;
using web_api_1.Contexts;
using web_api_1.Services.Interfaces;
using web_api_1.Services.Modules;
using web_api_1.Services.Modules.Interfaces;

namespace web_api_1.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private IItemService _itemService;
        private readonly IRedisService _redisService;
        private readonly IPostgreService _postgreService;
        private readonly PostgreDbContext _postgreDbContext;

        public UnitOfWork(IRedisService redisService, IPostgreService postgreService, PostgreDbContext postgreDbContext)
        {
            _redisService = redisService;
            _postgreService = postgreService;
            _postgreDbContext = postgreDbContext;
        }

        public IItemService ItemService => _itemService ??= new ItemService(_redisService, _postgreService, _postgreDbContext);

        public async Task CommitAsync()
        {
            await Task.CompletedTask;
        }
    }
}
