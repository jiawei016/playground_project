using KafkaConsumer;
using KafkaConsumer.Services.Repositories;
using KafkaConsumer.Services.Repositories.Interfaces;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    services.AddScoped<IKafkaService, KafkaService>();
    services.AddScoped<IRedisService, RedisService>();

    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.RunAsync();