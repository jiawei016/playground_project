using web_api_1.Services.Modules.Interfaces;

namespace web_api_1.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IItemService ItemService { get; }
        Task CommitAsync();
    }
}
