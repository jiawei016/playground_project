using web_api_1.Models;

namespace web_api_1.Services.Interfaces
{
    public interface IExpressService
    {
        Task<string> GetValueAsync(string key);
        Task<bool> SetValueAsync(UContentModel _UContentModel);
    }
}
