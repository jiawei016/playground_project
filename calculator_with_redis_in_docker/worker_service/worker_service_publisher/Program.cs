using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.EventLog;
using worker_service_publisher;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices(services =>
{
    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.RunAsync();