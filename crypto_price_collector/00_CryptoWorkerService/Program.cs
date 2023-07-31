using _00_CryptoWorkerService;
using _00_CryptoWorkerService.Service.Repo;
using _00_CryptoWorkerService.Service.Repo.Interface;
using _00_CryptoWorkerService.Service.UOW;
using _00_CryptoWorkerService.Service.UOW.Bitcoin_Price_Producer;

string _appSettingName = "";

//_appSettingName = "DockerCompose";
_appSettingName = "Kubernetes";

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureAppConfiguration((hostContext, config) =>
{
    config
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile($"appsettings.{_appSettingName}.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();
});
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

