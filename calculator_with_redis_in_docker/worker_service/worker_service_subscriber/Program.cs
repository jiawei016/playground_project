using worker_service_subscriber;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.RunAsync();
