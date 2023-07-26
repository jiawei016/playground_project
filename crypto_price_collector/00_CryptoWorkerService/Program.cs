using _00_CryptoWorkerService;
using _00_CryptoWorkerService.Service.Repo;
using _00_CryptoWorkerService.Service.Repo.Interface;
using _00_CryptoWorkerService.Service.UOW;
using _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    services.AddHttpClient("http_client", client =>
    {
        client.BaseAddress = new Uri("https://api.coingecko.com");
    });
    services.AddScoped<IUnitOfWork, UnitOfWork>();
    services.AddScoped<ICryptoService, CryptoService>();
    services.AddScoped<IKafkaService, KafkaService>();
    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.Run();

