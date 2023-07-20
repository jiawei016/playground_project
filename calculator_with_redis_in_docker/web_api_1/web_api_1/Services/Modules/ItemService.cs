using Newtonsoft.Json;
using web_api_1.Contexts;
using web_api_1.Models;
using web_api_1.Services.Interfaces;
using web_api_1.Services.Modules.Interfaces;

namespace web_api_1.Services.Modules
{
    public class ItemService : IItemService
    {
        private readonly IRedisService _redisService;
        private readonly IPostgreService _postgreService;
        private readonly PostgreDbContext _postgreDbContext;
        public ItemService(IRedisService redisService, IPostgreService postgreService, PostgreDbContext postgreDbContext)
        {
            _redisService = redisService;
            _postgreService = postgreService;
            _postgreDbContext = postgreDbContext;
        }
        public Task<string> GetValueAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveValueAsync(string key, string value)
        {
            ItemModel _model = new ItemModel
            {
                Id = Guid.NewGuid().ToString(),
                ChannelId = key,
                Content = value,
                IsProcessed = 0
            };

            string json = JsonConvert.SerializeObject(_model);

            await _redisService.SetValueAsync(key, json);
            await _postgreDbContext.ItemModel.AddAsync(_model);

            await _postgreDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SetIsProcessedAsync(string key, string Id)
        {
            var _value = await _postgreDbContext.ItemModel.FindAsync(Id);
            _value.IsProcessed = 1;

            await _postgreDbContext.SaveChangesAsync();

            return true;
        }
    }
}
