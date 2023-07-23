using KafkaConsumer;
using KafkaConsumer.Contexts;
using KafkaConsumer.Services.Repos;
using KafkaConsumer.Services.Repos.Interfaces;
using KafkaConsumer.Services.UOW;
using KafkaConsumer.Services.UOW.bitcoin_price_consumer;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((ctx, services) =>
{
    services.AddScoped<IKafkaService, KafkaService>();
    services.AddScoped<IRedisService, RedisService>();
    services.AddScoped<IBitcoinPriceConsumer, BitcoinPriceConsumer>();

    services.AddDbContext<PostgreDbContext>(options => {
        options.UseNpgsql(ctx.Configuration.GetSection("PostgreDBConfiguration").GetSection("ConnectionString").Value);
    });

    services.AddScoped<IUnitOfWork, UnitOfWork>();

    services.AddHostedService<Worker>();
});

IHost host = builder.Build();

host.RunAsync();