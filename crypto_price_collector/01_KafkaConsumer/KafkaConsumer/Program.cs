using KafkaConsumer;
using KafkaConsumer.Services.Repos;
using KafkaConsumer.Services.Repos.Interfaces;
using KafkaConsumer.Services.UOW;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IKafkaService, KafkaService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
