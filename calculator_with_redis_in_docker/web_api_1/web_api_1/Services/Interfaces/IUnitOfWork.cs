namespace web_api_1.Services.Interfaces
{
    public interface IUnitOfWork
    {
        IExpressService ExpressService { get; }
        Task CommitAsync();
    }
}
