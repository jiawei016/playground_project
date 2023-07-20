namespace web_api_1.Services.Modules.Interfaces
{
    public interface IItemService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SaveValueAsync(string key, string value);
        Task<bool> SetIsProcessedAsync(string key, string value);
    }
}
