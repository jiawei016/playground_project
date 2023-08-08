namespace api_voucher_client.Services.Repositories.Interfaces
{
    public interface IRedisService
    {
        public Task<string> GetRedisValue(string _key);
    }
}
