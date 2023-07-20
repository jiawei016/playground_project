using worker_service_subscriber;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    services.AddHttpClient("http_client", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5138/");
    });
    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.RunAsync();
