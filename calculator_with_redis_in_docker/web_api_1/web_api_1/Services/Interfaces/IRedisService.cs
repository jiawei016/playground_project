namespace web_api_1.Services.Interfaces
{
    public interface IRedisService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(string key, string value);
    }
}
